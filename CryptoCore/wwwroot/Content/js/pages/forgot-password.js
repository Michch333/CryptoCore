(function() {
  "use strict";
  // Instantiate text fields
  document.querySelectorAll('.mdc-text-field').forEach(text => new mdc.textField.MDCTextField(text));
})();