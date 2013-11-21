<%@ Application Language="C#" %>

<script runat="server">
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    void Application_Start(object sender, EventArgs e) 
    {
        log4net.Config.XmlConfigurator.Configure();

        System.Threading.Thread threadTwitterStream = new System.Threading.Thread(com.VotoVisible.Twitter.Stream.start);
        threadTwitterStream.IsBackground = true;
        threadTwitterStream.Start(); 
        Application["threadTwitterStream"] = threadTwitterStream;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        try
        {
            System.Threading.Thread threadTwitterStream = (System.Threading.Thread)Application["threadTwitterStream"];
            if (threadTwitterStream != null && threadTwitterStream.IsAlive == true)
            {
                threadTwitterStream.Abort();
            }
        }
        catch
        {
            ///
        }
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        Exception ex = HttpContext.Current.Server.GetLastError();
        if (log.IsFatalEnabled) log.Fatal("Unhandled error", ex);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        log4net.ThreadContext.Properties["addr"] = Request.UserHostAddress;
    }

    void Session_End(object sender, EventArgs e) 
    {
    }
</script>
