
'use strict';

goog.provide('Blockly.Blocks.hms_motion');

goog.require('Blockly.Blocks');


/**
 * Common HSV hue for all blocks in this category.
 */
Blockly.Blocks.hms_motion.HUE = 210;

Blockly.Blocks['hms_motion_moveforward'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("move")
        .appendField(new Blockly.FieldTextInput("0"), "numSteps")
        .appendField("steps");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);    
    this.setColour(210);
    this.setTooltip('Make the elephant go forward # steps');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_motion_turnleft'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("turnLeft");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('Makes the character turn 90 degrees left');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};

Blockly.Blocks['hms_motion_turnright'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("turnRight");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('Makes the character turn 90 degrees right');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};

Blockly.Blocks['hms_motion_eat'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("eat");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('Makes the character eat the banana at the current location');
    this.setHelpUrl('http://www.haathimerasaathi.org/');
  }
};

Blockly.Blocks['hms_motion_turn_clockwise'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("turn")
        .appendField(new Blockly.FieldImage("https://upload.wikimedia.org/wikipedia/en/a/a0/Clockwise_Icon.png", 15, 15, "*"))
        .appendField(new Blockly.FieldAngle(0), "ang");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_motion_turn_anti_clockwise'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("turn")
        .appendField(new Blockly.FieldImage("https://upload.wikimedia.org/wikipedia/en/8/88/Counter-clockwise_Icon.png", 15, 15, "*"))
        .appendField(new Blockly.FieldAngle(0), "ang");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};

Blockly.Blocks['hms_motion_gotoxy'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("goto x :")
        .appendField(new Blockly.FieldTextInput("0"), "x_val")
        .appendField("y :")
        .appendField(new Blockly.FieldTextInput("0"), "y_val");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip('');
    this.setHelpUrl('http://www.example.com/');
  }
};
























