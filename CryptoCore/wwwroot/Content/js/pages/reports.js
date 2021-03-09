(function() {
  "use strict";
  function calculateColors() {
    let primaryColor = window.getComputedStyle(document.documentElement).getPropertyValue('--mdc-theme-primary').trim();
    let secondaryColor = window.getComputedStyle(document.documentElement).getPropertyValue('--mdc-theme-secondary').trim();
    let secondaryColor700 = window.getComputedStyle(document.documentElement).getPropertyValue('--mdc-theme-secondary-700').trim();

    // Fallback for IE11, whereby getPropertyValue returns an empty string.
    primaryColor = '' === primaryColor ? '#ffce61' : primaryColor;
    secondaryColor = '' === secondaryColor ? '#f56c2a' : secondaryColor;
    secondaryColor700 = '' === secondaryColor700 ? '#f99a32' : secondaryColor700;
    return {primaryColor, secondaryColor, secondaryColor700};
  }

  function setupDashboardCharts() {
    const themeID = sessionStorage.getItem('crypto-html-theme');
    let chartFontColor = themeID && themeID === 'light-purple-red' ?
      'rgba(0, 0, 0, 0.65)' :
      'rgba(255, 255, 255, 0.45)';
    let colors = calculateColors();

    let options = {
      series: [
        {
          name: 'Bitcoin',
          data: [
            {x: new Date(1533514320000), y: 280},
            {x: new Date(1533600720000), y: 500},
            {x: new Date(1533687120000), y: 790},
            {x: new Date(1533773520000), y: 850},
            {x: new Date(1533859920000), y: 600},
            {x: new Date(1533946320000), y: 300},
            {x: new Date(1534032720000), y: 250}
          ]
        },
        {
          name: 'Ethereum',
          data: [
            {x: new Date(1533514320000), y: 400},
            {x: new Date(1533600720000), y: 450},
            {x: new Date(1533687120000), y: 650},
            {x: new Date(1533773520000), y: 810},
            {x: new Date(1533859920000), y: 500},
            {x: new Date(1533946320000), y: 280},
            {x: new Date(1534032720000), y: 800}
          ]
        },
        {
          name: 'Litecoin',
          data: [
            {x: new Date(1533514320000), y: 450},
            {x: new Date(1533600720000), y: 150},
            {x: new Date(1533687120000), y: 100},
            {x: new Date(1533773520000), y: 200},
            {x: new Date(1533859920000), y: 400},
            {x: new Date(1533946320000), y: 200},
            {x: new Date(1534032720000), y: 100}
          ]
        }
      ],
      colors: [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700],
      chart: {
        height: 500,
        foreColor: chartFontColor,
        type: 'line',
        animations: {
          easing: 'linear',
          dynamicAnimation: {
            speed: 1000
          }
        },
        toolbar: {
          show: false
        },
        zoom: {
          enabled: false
        }
      },
      grid: {
        borderColor: chartFontColor,
      },
      markers: {
        size: 3,
        opacity: 0.4,
        colors: [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700],
        strokeColor: '#fff',
        strokeWidth: 1,
        style: 'inverted', // full, hollow, inverted
        hover: {
          size: 5,
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'straight',
        width: 2,
      },
      xaxis: {
        type: 'datetime',
        tickAmount: 7,
        axisBorder: {
          show: true,
          color: 'rgba(255, 255, 255, 0.1)',
        },
      },
      legend: {
        show: true
      },
      tooltip: {
        x: {
          show: false
        },
        enabled: true,
        fillSeriesColor: true,
      }
    }
    let chart = new ApexCharts(
      document.querySelector('#performance-charts'),
      options
    );
    chart.render();

    // Changes the data when on weekly view.
    let lastDate = new Date(1534032720000);
    function dynamicDataHandler() {
      let newDate = lastDate.setDate(lastDate.getDate() + 1);
      chart.appendData([{
          data: [{x: newDate, y: Math.floor((Math.random() * 250) + 250)}]
      }, {
          data: [{x: newDate, y: Math.floor((Math.random() * 800) + 800)}]
      }, {
          data: [{x: newDate, y: Math.floor((Math.random() * 100) + 100)}]
      }]);
      chart.opts.series[0].data.shift();
      chart.opts.series[1].data.shift();
      chart.opts.series[2].data.shift();
    }
    // We store the id of the timer so we can clear the interval on monthly and yearly graphs.
    let timedInterval = window.setInterval(dynamicDataHandler, 4000);

    document.querySelector('.data-monthly-refresh').addEventListener('click', function (e) {
      clearInterval(timedInterval);
      chart.updateOptions({
        series: [
          {
            name: 'Bitcoin',
            data: [
              {x: new Date(1515370320000), y: 200},
              {x: new Date(1518048720000), y: 400},
              {x: new Date(1520467920000), y: 690},
              {x: new Date(1523146320000), y: 450},
              {x: new Date(1525738320000), y: 267},
              {x: new Date(1528416720000), y: 300},
              {x: new Date(1531008720000), y: 150}
            ]
          },
          {
            name: 'Ethereum',
            data: [
              {x: new Date(1515370320000), y: 340},
              {x: new Date(1518048720000), y: 450},
              {x: new Date(1520467920000), y: 750},
              {x: new Date(1523146320000), y: 510},
              {x: new Date(1525738320000), y: 300},
              {x: new Date(1528416720000), y: 200},
              {x: new Date(1531008720000), y: 150}
            ]
          },
          {
            name: 'Litecoin',
            data: [
              {x: new Date(1515370320000), y: 250},
              {x: new Date(1518048720000), y: 650},
              {x: new Date(1520467920000), y: 400},
              {x: new Date(1523146320000), y: 230},
              {x: new Date(1525738320000), y: 70},
              {x: new Date(1528416720000), y: 690},
              {x: new Date(1531008720000), y: 810}
            ]
          }
        ],
      }, false, true, false);
      e.preventDefault();
    });

    document.querySelector('.data-yearly-refresh').addEventListener('click', function (e) {
      clearInterval(timedInterval);
      chart.updateOptions({
        series: [
          {
            name: 'Bitcoin',
            data: [
              {x: new Date(1341706320000), y: 690},
              {x: new Date(1373242320000), y: 450},
              {x: new Date(1404778320000), y: 267},
              {x: new Date(1436314320000), y: 300},
              {x: new Date(1467936720000), y: 150},
              {x: new Date(1499472720000), y: 550},
              {x: new Date(1531008720000), y: 630}
            ]
          },
          {
            name: 'Ethereum',
            data: [
              {x: new Date(1341706320000), y: 380},
              {x: new Date(1373242320000), y: 150},
              {x: new Date(1404778320000), y: 650},
              {x: new Date(1436314320000), y: 560},
              {x: new Date(1467936720000), y: 390},
              {x: new Date(1499472720000), y: 100},
              {x: new Date(1531008720000), y: 190}
            ]
          },
          {
            name: 'Litecoin',
            data: [
              {x: new Date(1341706320000), y: 450},
              {x: new Date(1373242320000), y: 550},
              {x: new Date(1404778320000), y: 100},
              {x: new Date(1436314320000), y: 430},
              {x: new Date(1467936720000), y: 700},
              {x: new Date(1499472720000), y: 530},
              {x: new Date(1531008720000), y: 810}
            ]
          }
        ],
      }, false, true, false);
      e.preventDefault();
    });

    document.querySelector('.data-weekly-refresh').addEventListener('click', function (e) {
      clearInterval(timedInterval);
      lastDate = new Date(1534032720000);
      timedInterval = window.setInterval(dynamicDataHandler, 4000);
      chart.updateOptions({
        series: [
          {
            name: 'Bitcoin',
            data: [
              {x: new Date(1533514320000), y: 280},
              {x: new Date(1533600720000), y: 500},
              {x: new Date(1533687120000), y: 790},
              {x: new Date(1533773520000), y: 850},
              {x: new Date(1533859920000), y: 600},
              {x: new Date(1533946320000), y: 300},
              {x: new Date(1534032720000), y: 250}
            ]
          },
          {
            name: 'Ethereum',
            data: [
              {x: new Date(1533514320000), y: 400},
              {x: new Date(1533600720000), y: 450},
              {x: new Date(1533687120000), y: 650},
              {x: new Date(1533773520000), y: 810},
              {x: new Date(1533859920000), y: 500},
              {x: new Date(1533946320000), y: 280},
              {x: new Date(1534032720000), y: 800}
            ]
          },
          {
            name: 'Litecoin',
            data: [
              {x: new Date(1533514320000), y: 450},
              {x: new Date(1533600720000), y: 150},
              {x: new Date(1533687120000), y: 100},
              {x: new Date(1533773520000), y: 200},
              {x: new Date(1533859920000), y: 400},
              {x: new Date(1533946320000), y: 200},
              {x: new Date(1534032720000), y: 100}
            ]
          }
        ]
      }, false, true, false);
      e.preventDefault();
    });

    // Exchange rate chart

    let exchangeOptions = {
      series: [
        {
          name: 'Bitcoin',
          data: [
            {x: new Date(1533514320000), y: 180},
            {x: new Date(1533600720000), y: 550},
            {x: new Date(1533687120000), y: 400},
            {x: new Date(1533773520000), y: 580},
            {x: new Date(1533859920000), y: 150}
          ]
        }
      ],
      colors: colors.primaryColor,
      chart: {
        height: 170,
        width: '100%',
        foreColor: chartFontColor,
        type: 'area',
        toolbar: {
          show: false
        },
        zoom: {
          enabled: false
        }
      },
      grid: {
        show: false
      },
      markers: {
        size: 3,
        opacity: 0.4,
        colors: 'transparent',
        strokeColor: colors.primaryColor,
        strokeWidth: 2,
        style: 'inverted', // full, hollow, inverted
        hover: {
          size: 3,
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'straigth',
        width: 2,
      },
      xaxis: {
        type: 'datetime',
        tickAmount: 7,
        axisBorder: {
          show: true,
          color: 'rgba(255, 255, 255, 0.1)',
        },
        tooltip: {
          enabled: false
        },
        labels: {
          datetimeFormatter: {
            day: 'dd/M'
          },
        }
      },
      yaxis: {
        labels: {
          show: false
        }
      },
      tooltip: {
        x: {
          show: false
        },
        y: {
          show: false
        },
        enabled: true,
        fillSeriesColor: true,
      }
    }
    let exchangeChart = new ApexCharts(
      document.querySelector('#chart-exchange'),
      exchangeOptions
    );
    exchangeChart.render();

    // Coin chart options
    let chartConfig = {
      colors: colors.primaryColor,
      chart: {
        height: 114,
        width: 140,
        foreColor: chartFontColor,
        type: 'line',
        sparkline: {
          enabled: true
        },
        toolbar: {
          show: false
        },
        zoom: {
          enabled: false
        }
      },
      grid: {
        show: false
      },
      markers: {
        size: 0
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'smooth',
        width: 2,
      },
      xaxis: {
        type: 'datetime',
        tickAmount: 7,
        axisBorder: {
          show: false
        },
        labels: {
          show: false
        }
      },
      yaxis: {
        labels: {
          show: false,
        },
        axisBorder: {
          show: false
        }
      },
      tooltip: {
        enabled: false
      }
    }
    const btcCoinChartOptions = Object.assign({}, {
      series: [
        {
          name: 'Bitcoin',
          data: [
            {x: new Date(1533514320000), y: 80},
            {x: new Date(1533600720000), y: 200},
            {x: new Date(1533687120000), y: 400},
            {x: new Date(1533773520000), y: 250},
            {x: new Date(1533859920000), y: 650}
          ]
        }
      ]
    }, chartConfig);
    // Coin Performance section - Btc widget
    let btcChart = new ApexCharts(
      document.querySelector('.crypto-coin__chart--btc'),
      btcCoinChartOptions
    );
    btcChart.render();

    // Coin Performance section - Eth widget
    const ethCoinChartOptions = Object.assign({}, {
      series: [
        {
          name: 'Ethereum',
          data: [
            {x: new Date(1533514320000), y: 80},
            {x: new Date(1533600720000), y: 100},
            {x: new Date(1533687120000), y: 110},
            {x: new Date(1533773520000), y: 120}
          ]
        }
      ]
    }, chartConfig);
    // Coin Performance section - Eth widget
    let ethChart = new ApexCharts(
      document.querySelector('.crypto-coin__chart--eth'),
      ethCoinChartOptions
    );
    ethChart.render();

    // Coin Performance section - Ltc widget
    const ltcCoinChartOptions = Object.assign({}, {
      series: [
        {
          name: 'Litecoin',
          data: [
            {x: new Date(1533514320000), y: 80},
            {x: new Date(1533600720000), y: 100},
            {x: new Date(1533687120000), y: 130},
            {x: new Date(1533773520000), y: 140},
            {x: new Date(1533859920000), y: 30}
          ]
        }
      ]
    }, chartConfig);
    // Coin Performance section - Ltc widget
    let ltcChart = new ApexCharts(
      document.querySelector('.crypto-coin__chart--ltc'),
      ltcCoinChartOptions
    );
    ltcChart.render();

    // Performance Reports section - Pie chart widget
    var performanceOptions = {
      chart: {
        height: 500,
        type: 'line',
        stacked: false,
        foreColor: chartFontColor,
      },
      colors: [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700],
      stroke: {
        width: [0, 2, 3],
        curve: 'smooth'
      },
      plotOptions: {
        bar: {
          columnWidth: '50%'
        }
      },
      series: [{
        name: 'Bitcoin',
        type: 'column',
        data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30]
      }, {
        name: 'Ethereum',
        type: 'area',
        data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43]
      }, {
        name: 'Litecoin',
        type: 'line',
        data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39]
      }],
      fill: {
        opacity: [0.85,0.25,1],
        gradient: {
          inverseColors: false,
          shade: 'light',
          type: "vertical",
          opacityFrom: 0.85,
          opacityTo: 0.55,
          stops: [0, 100, 100, 100]
        }
      },
      labels: ['01/01/2018', '02/01/2018','03/01/2018','04/01/2018','05/01/2018','06/01/2018','07/01/2018','08/01/2018','09/01/2018','10/01/2018','11/01/2018'],
      markers: {
        size: 0
      },
      xaxis: {
        type:'datetime'
      },
      yaxis: {
        title: {
          text: 'Points',
        },
      },
      grid: {
        borderColor: chartFontColor,
      },
      tooltip: {
        shared: true,
        intersect: false,
        x: {
          show: false
        },
        y: {
          formatter: function (y) {
            if(typeof y !== "undefined") {
              return  y.toFixed(0) + " points";
            }
            return y;
          }
        }
      }
    }

    var performanceChart = new ApexCharts(
      document.querySelector('#performance-report'),
      performanceOptions
    );

    performanceChart.render();
    window.setInterval(function () {
      performanceChart.updateSeries([{
      name: 'Bitcoin',
      type: 'column',
      data: [
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100)),
        Math.floor((Math.random() * 100))
      ]
      }, {
        name: 'Ethereum',
        type: 'area',
        data: [
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100))
        ]
      }, {
        name: 'Litecoin',
        type: 'line',
        data: [
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100)),
          Math.floor((Math.random() * 100))
        ]
      }])
    }, 6000);

    // We need to reload the charts because we switched skin.
    const body = document.getElementsByTagName('body').item(0);
    body.addEventListener('cryptoThemeChanged', () =>
      {
        const themeID = sessionStorage.getItem('crypto-html-theme');
        let chartFontColor = themeID && themeID === 'light-purple-red' ?
          'rgba(0, 0, 0, 0.65)' :
          'rgba(255, 255, 255, 0.45)';
        let colors = calculateColors();

        chart.updateOptions({
          colors: [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700],
          markers: {
            colors : [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700]
          },
          chart: {
            foreColor: chartFontColor
          },
          grid: {
            borderColor: chartFontColor,
          }
        }, false, true, false);

        exchangeChart.updateOptions({
          colors: colors.primaryColor,
          markers: {
            strokeColor : colors.primaryColor,
          },
          chart: {
            foreColor: chartFontColor
          }
        }, false, true, false);

        performanceChart.updateOptions({
          colors: [colors.primaryColor, colors.secondaryColor, colors.secondaryColor700],
          chart: {
            foreColor: chartFontColor
          },
          grid: {
            borderColor: chartFontColor,
          }
        }, false, true, false);
      }
    );
  } // end of setupDashboardCharts.

  // Wait 1sec before initializing the charts so the switcher CSS works.
  setTimeout(setupDashboardCharts, 1000);

  const coinSelect = document.querySelector('#coin-select');
  const currencySelect = document.querySelector('#currency-select');
  if (coinSelect) {
    new mdc.select.MDCSelect(coinSelect);
  }
  if (currencySelect) {
    new mdc.select.MDCSelect(currencySelect);
  }

  // Instantiate text fields
  document.querySelectorAll('.mdc-text-field').forEach(text => new mdc.textField.MDCTextField(text));
})();