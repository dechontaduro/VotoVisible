var colors = { 'Sí': 'green', 'No': 'red' };
var personURL = '../images/man_black_256.png';
function drawResultados() {
    //d3.select(window).on('resize', resize);
    var margin = { top: 1, left: 1, bottom: 1, right: 1 }
      , width = parseInt(d3.select('#svgResultados').style('width'))
      , width = width - margin.left - margin.right
      , mapRatio = 0.9
      , height = width * mapRatio
      , radioMax = width / 4;

    var svg = d3.select('#svgResultados')
                            .append('svg')
                            .attr('width', width)
                            .attr('height', height);

    var groups = svg.selectAll('g')
        .data(resultados)
        .enter()
        .append('g');

    //groups.translate([width / 2, height / 2]);
    groups.attr('transform', function (d, i) {
        var x = (width / 2) * (i);
        var y = 0;//(height / 2) * (1 + i);
        return 'translate(' + [x, y] + ')';
    });

    var images = groups.append('image')
                .attr('xlink:href', personURL)
                //.attr('width', function (d, i) { return d.total * (radioMax / 3); })
                //.attr('alignment-baseline', 'top')
                //.style('z-index', '10')
                .attr('width', function (d, i) { return d.total * (radioMax / 1); })
                .attr('height', function (d, i) { return d.total * (radioMax / 1); })

    var label = groups.append('text')
    .text(function (d) { return d.decision; })
    //.attr('x', function (d, i) { return d.total * (radioMax / 1); })
    .attr('x', 65)
    .attr('y', 55)
    .attr('alignment-baseline', 'top')
    .attr('text-anchor', 'middle')
    .style('font-size', '22px')
    .style('fill', '#ffffff')


    var label = groups.append('text')
    .text(function (d) { return d.total; })
    .attr('x', 65)
    .attr('y', 25)
    .attr('alignment-baseline', 'middle')
    .attr('text-anchor', 'middle')
    .style('font-size', '18px')
    .style('fill', '#ffffff')


    //.style('z-index', '500')
    //.style('font-size', function (d) { return (2 * d.r - 8) / this.getComputedTextLength() * 50 + 'px'; });

    

    /*
    var totales = groups.append('circle')
    .attr({
        cx: function (d, i) { return 0; },
        cy: function (d, i) { return 0; },
        r: function (d, i) { return d.total * (radioMax / 3); },
        fill: function (d, i) {
            return colors[d.decision];
        },
        stroke: '#2F3550',
        'stroke-width': 2.4192
    });
    */
    /*
    var privados = groups.append('circle')
    .attr({
        cx: function (d, i) { return 0; },
        cy: function (d, i) { return 0; },
        r: function (d, i) { return d.privados * (radioMax / 3); },
        fill: function (d, i) { return 'gray'; }
    });
    */
    

    


    /*
    var totales = svgContainer.selectAll('circle')
                              .data(resultados)
                              .enter()
                              .append('circle');
    var cx = 0.0;
    var cy = 50.0;
    var pTotales = totales
        .attr('cx', 0)
        .attr('cy', 0)
        .attr('r', 0)
        .text('SIIIII')
        .style('fill', function(d) {
            return colors[d.decision];
        })
        .style('opacity', 0)
        .transition()
            .attr('cx', function (d) {
                cx += radioMax;
                return cx;
            })
            .attr('cy', function (d) {
                cy += 20;
                return cy;
            })
            .attr('r', function (d) {
                return (d.total * (radioMax / 3));
            })
          .style('opacity', 1)
          .duration(3000) // this is 1s
          .delay(400);
*/
    /*
    var privados = svgContainer.selectAll('circle')
                              .data(resultados)
                              .enter()
                              .append('circle');
    cx = 0.0;
    cy = 0.0;
    privados
        .attr('cx', function (d) {
            cx += radioMax;
            return cx;
        })
        .attr('cy', function (d) {
            cy += 20;
            return cy;
        })
        .attr('r', function (d) {
            return (d.privados * (radioMax / 3));
        })
        .text(function (d) { d.decision })
        .style('fill', function (d) {
            return 'gray';
        });*/
}