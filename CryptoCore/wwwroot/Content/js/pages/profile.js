(function() {
  "use strict";
  const panels = document.querySelector('.settings-form-panels');
  const settingsLinkItems = document.querySelectorAll('.crypto-border-list__item');
  const settingsLinksContainer = document.querySelector('.crypto-border-list');
  settingsLinksContainer.addEventListener('click', function(e) {
    settingsLinkItems.forEach((item, i) => {
      item.classList.remove('crypto-border-list__item--active');
    });

    let element = e.target;
    while (element !== e.currentTarget) {
      if (element.classList.contains('crypto-border-list__item')) {
        element.classList.add('crypto-border-list__item--active');
        let index = Array.prototype.findIndex.call(settingsLinkItems, link => link.classList.contains('crypto-border-list__item--active'));
        cryptoUpdateFormPanels(panels, index);
        e.preventDefault();
        return;
      }
      element = element.parentNode;
    }
  }, false);

  function cryptoUpdateFormPanels(panels, index) {
    const activePanel = panels.querySelector('.crypto-settings-form-panel.crypto-settings-form-panel--active');
    if (activePanel) {
      activePanel.classList.remove('crypto-settings-form-panel--active');
    }
    const newActivePanel = panels.querySelector('.crypto-settings-form-panel:nth-child(' + (index + 1) + ')');
    if (newActivePanel) {
      newActivePanel.classList.add('crypto-settings-form-panel--active');
    }
  }
  // Instantiate text fields
  document.querySelectorAll('.mdc-text-field').forEach(text => new mdc.textField.MDCTextField(text));

  // Instantiate switches
  document.querySelectorAll('.crypto-switch-selector').forEach(switcher => new mdc.switchControl.MDCSwitch(switcher));
})();