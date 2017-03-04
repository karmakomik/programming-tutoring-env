'use strict';

goog.provide('Blockly.Blocks.hms_events');

goog.require('Blockly.Blocks');


/**
 * Common HSV hue for all blocks in this category.
 */
Blockly.Blocks.hms_events.HUE = 30;
Blockly.BlockSvg.START_HAT = true;
/*Blockly.HSV_SATURATION = 0.83;
Blockly.HSV_VALUE = 0.95;*/


Blockly.Blocks['hms_events_when_object_clicked'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("when this object clicked");
    this.setPreviousStatement(false, null); //For Hat look to take effect
    this.setNextStatement(true, null);
    this.setColour(30);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};