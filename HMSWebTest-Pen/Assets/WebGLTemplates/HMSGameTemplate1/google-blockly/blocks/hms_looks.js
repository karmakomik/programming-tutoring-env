
'use strict';

goog.provide('Blockly.Blocks.hms_looks');

goog.require('Blockly.Blocks');


/**
 * Common HSV hue for all blocks in this category.
 */
Blockly.Blocks.hms_looks.HUE = 160;

Blockly.Blocks['hms_looks_say'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("say")
        .appendField(new Blockly.FieldTextInput("Hello!"), "sayMsg");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};

Blockly.Blocks['hms_looks_say_for_time'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("say")
        .appendField(new Blockly.FieldTextInput("Hello!"), "sayMsg")
        .appendField("for")
        .appendField(new Blockly.FieldTextInput("0"), "time")
        .appendField("secs");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_looks_think'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("think")
        .appendField(new Blockly.FieldTextInput("Hello!"), "sayMsg");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};

Blockly.Blocks['hms_looks_think_for_time'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("think")
        .appendField(new Blockly.FieldTextInput("Hello!"), "sayMsg")
        .appendField("for")
        .appendField(new Blockly.FieldTextInput("0"), "time")
        .appendField("secs");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_looks_change_costume'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("change costume to")
        .appendField(new Blockly.FieldDropdown([["1", "c1"], ["2", "c2"], ["3", "c3"], ["4", "c4"], ["5", "c5"]]), "costumeID");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_looks_show'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("show");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_looks_hide'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("hide");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(270);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};























