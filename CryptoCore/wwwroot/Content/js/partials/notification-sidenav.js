(function() {
  "use strict";
  // Notifications Drawer
  const notificationDrawerEl = document.querySelector('#notifications-drawer');
  const notificationDrawer = new mdc.drawer.MDCTemporaryDrawer(notificationDrawerEl);

  document.querySelector('#crypto-top-toolbar-notification-icon').addEventListener('click', function() {
    notificationDrawer.open = true;
  });
  document.querySelector('.crypto-drawer-notification-collapse-icon').addEventListener('click', function(e) {
    notificationDrawer.open = false;
    e.preventDefault();
  });

  // Notification sidenav tabs
  const notificationTabBar = new mdc.tabs.MDCTabBar(document.querySelector('#notification-tab-bar'));
  notificationTabBar.layout();
  notificationTabBar.listen('MDCTabBar:change', function (t) {
    const panelTabBar = t.detail;
    const index = panelTabBar.activeTabIndex;
    const panels = document.querySelector('#notification-panels');
    cryptoUpdateTabPanels(panels, index);
  });

  function cryptoUpdateTabPanels(panels, index) {
    const activePanel = panels.querySelector('.crypto-tab-panel.crypto-tab-panel--active');
    if (activePanel) {
      activePanel.classList.remove('crypto-tab-panel--active');
    }
    const newActivePanel = panels.querySelector('.crypto-tab-panel:nth-child(' + (index + 1) + ')');
    if (newActivePanel) {
      newActivePanel.classList.add('crypto-tab-panel--active');
    }
  }

  const body = document.getElementsByTagName('body').item(0);
  // We need to relayout all tabs when switching to rtl.
  body.addEventListener('cryptoDirectionChanged', function () {
    notificationTabBar.layout();
  });
})();
