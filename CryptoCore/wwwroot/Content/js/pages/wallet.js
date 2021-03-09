(function() {
  "use strict";
  function calculatePrimaryColor() {
    let primaryColor = window.getComputedStyle(document.documentElement).getPropertyValue('--mdc-theme-primary').trim();

    // Fallback for IE11, whereby getPropertyValue returns an empty string.
    return '' === primaryColor ? '#ffce61' : primaryColor;
  }

  function setupDashboardCharts() {
    const themeID = sessionStorage.getItem('crypto-html-theme');
    let primaryColor = calculatePrimaryColor();
    let chartFontColor = themeID && themeID === 'light-purple-red' ?
      'rgba(0, 0, 0, 0.65)' :
      'rgba(255, 255, 255, 0.45)';

    let options = {
      series: [
        {
          name: "Bitcoin",
          data: [
            {x: new Date(1533514320000), y: 280},
            {x: new Date(1533600720000), y: 500},
            {x: new Date(1533687120000), y: 790},
            {x: new Date(1533773520000), y: 850},
            {x: new Date(1533859920000), y: 600},
            {x: new Date(1533946320000), y: 300},
            {x: new Date(1534032720000), y: 250}
          ]
        }
      ],
      colors: primaryColor,
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
        colors: primaryColor,
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
        curve: 'smooth',
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
        }
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
      document.querySelector('#wallet-performance-chart'),
      options
    );
    chart.render();

    // Changes the data when on weekly view.
    let lastDate = new Date(1534032720000);
    function dynamicDataHandler() {
      let newDate = lastDate.setDate(lastDate.getDate() + 1);
      chart.appendData([{
          data: [{x: newDate, y: Math.floor((Math.random() * 250) + 250)}]
      }]);
      chart.opts.series[0].data.shift();
    }
    // We store the id of the timer so we can clear the interval on monthly and yearly graphs.
    let timedInterval = window.setInterval(dynamicDataHandler, 4000);

    document.querySelector('.data-monthly-refresh').addEventListener('click', function (e) {
      clearInterval(timedInterval);
      chart.updateOptions({
        series: [
          {
            name: "Bitcoin",
            data: [
              {x: new Date(1515370320000), y: 200},
              {x: new Date(1518048720000), y: 400},
              {x: new Date(1520467920000), y: 690},
              {x: new Date(1523146320000), y: 450},
              {x: new Date(1525738320000), y: 267},
              {x: new Date(1528416720000), y: 300},
              {x: new Date(1531008720000), y: 150}
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
            name: "Bitcoin",
            data: [
              {x: new Date(1341706320000), y: 690},
              {x: new Date(1373242320000), y: 450},
              {x: new Date(1404778320000), y: 267},
              {x: new Date(1436314320000), y: 300},
              {x: new Date(1467936720000), y: 150},
              {x: new Date(1499472720000), y: 550},
              {x: new Date(1531008720000), y: 630}
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
            name: "Bitcoin",
            data: [
              {x: new Date(1533514320000), y: 280},
              {x: new Date(1533600720000), y: 500},
              {x: new Date(1533687120000), y: 790},
              {x: new Date(1533773520000), y: 850},
              {x: new Date(1533859920000), y: 600},
              {x: new Date(1533946320000), y: 300},
              {x: new Date(1534032720000), y: 250}
            ]
          }
        ]
      }, false, true, false);
      e.preventDefault();
    });

    // We need to reload the charts because we switched skin.
    const body = document.getElementsByTagName('body').item(0);
    body.addEventListener('cryptoThemeChanged', () =>
      {
        const themeID = sessionStorage.getItem('crypto-html-theme');
        let chartFontColor = themeID && themeID === 'light-purple-red' ?
          'rgba(0, 0, 0, 0.65)' :
          'rgba(255, 255, 255, 0.45)';
        let primaryColor = calculatePrimaryColor();
        chart.updateOptions({
          colors: primaryColor,
          markers: {
            colors : primaryColor
          },
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

  // Analytics tabs listener. Datasets refresh on TabBarChange.
  const transactionTabBar = new mdc.tabBar.MDCTabBar(document.querySelector('#crypto-wallet-transaction'));
  // Client acquisition tabs listener.
  transactionTabBar.listen('MDCTabBar:activated', function (t) {
    const detailTabBar = t.detail;
    const index = detailTabBar.index;
    const panels = document.querySelector('#wallet-form-tabs');

    cryptoUpdateTabPanels(panels, index);
  });


  function cryptoUpdateTabPanels(panels, index) {
    const activePanel = panels.querySelector('.crypto-wallet-form-panel.crypto-wallet-form-panel--active');
    if (activePanel) {
      activePanel.classList.remove('crypto-wallet-form-panel--active');
    }
    const newActivePanel = panels.querySelector('.crypto-wallet-form-panel:nth-child(' + (index + 1) + ')');
    if (newActivePanel) {
      newActivePanel.classList.add('crypto-wallet-form-panel--active');
    }
  }

  const tableExpandTogglesContainer = document.querySelector('.crypto-widget__content .mdl-data-table');
  tableExpandTogglesContainer.addEventListener('click', function(e){
    let element = e.target;
    while (element !== e.currentTarget) {
      if (element.classList.contains('crypto-transactions-list__item-toggle')) {
        element.classList.toggle('rotated');
        element.parentNode.parentNode.nextElementSibling.classList.toggle('expanded');
        e.preventDefault();
        return;
      }
      element = element.parentNode;
    }
  });

  // Instantiate text fields
  document.querySelectorAll('.mdc-text-field').forEach(text => new mdc.textField.MDCTextField(text));

  const menuEl = document.querySelector('#widget-menu');
  const menu = new mdc.menu.MDCMenu(menuEl);
  const menuButtonEl = document.querySelector('#menu-button');
  menuButtonEl.addEventListener('click', function() {
    menu.open = !menu.open;
  });
  menu.setAnchorMargin({left: -60});

})();