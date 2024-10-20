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

function lineChart(data) {
    var options = {
        series: data.series,
        
        chart: {
            height: 350,
            type: 'line',
            zoom: {
                enabled: false
            }
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'straight'
        },
        title: {
            text: 'Sales trends by created date',
            align: 'left'
        },
        grid: {
            row: {
                colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.5
            },
        },
        xaxis: {
            categories: data.categories,
        }
    };

    var chart = new ApexCharts(document.querySelector("#LineChart"), options);
    chart.render();
}