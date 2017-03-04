'use strict';

goog.provide('Blockly.Blocks.hms_pen');

goog.require('Blockly.Blocks');


/**
 * Common HSV hue for all blocks in this category.
 */
Blockly.Blocks.hms_pen.HUE = 90;

Blockly.Blocks['hms_pen_down'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("pen down");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(90);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_pen_up'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("pen up");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(90);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_pen_clear'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("clear");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(90);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_pen_set_color'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("set pen color to")
        .appendField(new Blockly.FieldColour("#993300"), "colorID");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(90);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_pen_set_size'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("set pen size to")
        .appendField(new Blockly.FieldTextInput("0"), "penSize");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(90);
    this.setTooltip('');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};