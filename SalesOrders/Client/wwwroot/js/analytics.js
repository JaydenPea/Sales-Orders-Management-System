function renderChart(data) {
    var options = {
        series: data.series, // Use the data passed from Blazor
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: data.labels, // Use labels passed from Blazor
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();
}
