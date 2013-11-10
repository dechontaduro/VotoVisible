﻿using System;
using System.Collections.Generic;

namespace com.VotoVisible.Entitity
{
    /// <summary>
    /// Descripción breve de Votacion
    /// </summary>
    public class Votacion
    {
        public int Id
        {
            get{ return id;}
            set{id = value;}
        }

        public string Corporacion
        {
            get { 
                return corporacion; 
            }
            set { corporacion = value; }
        }

        public int id;
        public string corporacion;
        public string titulo;
        public string numero;
        public string anio;
        public string url;
        public string twitterAccount;
        public string tweetId;
        public DateTime? creado;
        public DateTime? finalizado;
        public List<Voto> votos;
        public List<VotacionTotales> totales;
        public Voto mostRetweet;
        public Voto mostReply;

        public Votacion() { }
        public Votacion(int id, 
                        string corporacion,
                        string titulo,
                        string numero,
                        string anio,
                        string url,
                        string twitterAccount,
                        string tweetId,
                        DateTime? creado,
                        DateTime? finalizado)
        {
            this.id = id;
            this.corporacion = corporacion;
            this.titulo = titulo;
            this.numero = numero;
            this.anio = anio;
            this.url = url;
            this.twitterAccount = twitterAccount;
            this.tweetId = tweetId;
            this.creado = creado;
            this.finalizado = finalizado;
        }
    }
}