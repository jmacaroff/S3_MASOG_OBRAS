import * as Chart from "../plugins/chart.js/Chart";

$(function () {
    'use strict'
    var ticksStyle = {
        fontColor: '#495057',
        fontStyle: 'bold'
    }

    var mode = 'index'
    var intersect = true

    var myAmountEntry = [];
    var myAmountEgress = [];
    var myMonthList = [];
    var myResum;

    function showChart() {
        myAmountEntry = myResum.AmountEntryList;
        myAmountEgress = myResum.AmountEgressList;
        myMonthList = myResum.MonthList;

        console.log(myAmountEntry);
        console.log(myAmountEgress);

        var $visitorsChart = $('#ingresosegresos-chart')
        var visitorsChart = new Chart($visitorsChart, {
            type: 'line',
            data: {
                labels: myMonthList,
                datasets: [{
                    label: 'Ingresos',             
                    data:  myAmountEntry,
                    backgroundColor: 'transparent',
                    borderColor: '#007bff',
                    pointBorderColor: '#007bff',
                    pointBackgroundColor: '#007bff',
                    fill: false
                    // pointHoverBackgroundColor: '#007bff',
                    // pointHoverBorderColor    : '#007bff'
                },
                {
                    label: 'Egresos',
                    type: 'line',
                    data: myAmountEgress,
                    backgroundColor: 'tansparent',
                    borderColor: '#ced4da',
                    pointBorderColor: '#ced4da',
                    pointBackgroundColor: '#ced4da',
                    fill: false
                    // pointHoverBackgroundColor: '#ced4da',
                    // pointHoverBorderColor    : '#ced4da'
                }]
            },
            options: {
                maintainAspectRatio: false,
                tooltips: {
                    mode: mode,
                    intersect: intersect
                },
                hover: {
                    mode: mode,
                    intersect: intersect
                },
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        // display: false,
                        gridLines: {
                            display: true,
                            lineWidth: '4px',
                            color: 'rgba(0, 0, 0, .2)',
                            zeroLineColor: 'transparent'
                        },
                        ticks: $.extend({
                            beginAtZero: true,
                            suggestedMax: 200
                        }, ticksStyle)
                    }],
                    xAxes: [{
                        display: true,
                        gridLines: {
                            display: false
                        },
                        ticks: ticksStyle
                    }]
                }
            }
        });

    }
   


    function getChartData() {
        return fetch('./Dashboard?handler=InvoiceChartData',
            {
                method: 'get',
                headers: {
                    'Content-Type': 'application/json;charset=UTF-8'
                }
            })
            .then(function (response) {
                if (response.ok) {
                    return response.text();
                } else {
                    throw Error('Response Not OK');
                }
            })
            .then(function (text) {
                try {
                    return JSON.parse(text);
                } catch (err) {
                    throw Error('Method Not Found');
                }
            })
            .then(function (responseJSON) {
                myResum = responseJSON;
                showChart();
            })
    }

    getChartData();
})
