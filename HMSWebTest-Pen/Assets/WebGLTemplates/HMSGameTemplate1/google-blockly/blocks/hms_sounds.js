'use strict';

goog.provide('Blockly.Blocks.hms_sounds');

goog.require('Blockly.Blocks');


/**
 * Common HSV hue for all blocks in this category.
 */
Blockly.Blocks.hms_sounds.HUE = 160;

Blockly.Blocks['hms_sounds_play'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("play sound")
        .appendField(new Blockly.FieldDropdown([["1", "s1"], ["2", "s2"], ["3", "s3"], ["4", "s4"], ["5", "s5"]]), "soundID");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(315);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};