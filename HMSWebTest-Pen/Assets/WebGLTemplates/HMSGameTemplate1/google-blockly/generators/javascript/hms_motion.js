/**
 * @fileoverview Generating JavaScript for Haathi Mera Saathi blocks.
 * @author unni.r.krishnan@gmail.com (Unnikrishnan R)
 */
'use strict';

goog.provide('Blockly.JavaScript.hms_motion');

goog.require('Blockly.JavaScript');

Blockly.JavaScript['hms_motion_moveforward'] = function(block) 
{
  var text_numsteps = block.getFieldValue('numSteps');
  // TODO: Assemble JavaScript into code variable.
  var code = "moveForward(\"" + text_numsteps + "\");\n";
  return code;
};

Blockly.JavaScript['hms_motion_turnleft'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'turnLeft();\n';
  return code;
};

Blockly.JavaScript['hms_motion_turnright'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'turnRight();\n';
  return code;
};

Blockly.JavaScript['hms_motion_eat'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'eat();\n';
  return code;
};

Blockly.JavaScript['hms_motion_turn_clockwise'] = function(block) {
  var angle_ang = block.getFieldValue('ang');
  // TODO: Assemble JavaScript into code variable.
  var code = "turnClockwise(" + angle_ang + ");\n"; 
  return code;
};

Blockly.JavaScript['hms_motion_turn_anti_clockwise'] = function(block) {
  var angle_ang = block.getFieldValue('ang');
  // TODO: Assemble JavaScript into code variable.
  var code = "turnAntiClockwise(" + angle_ang + ");\n"; 
  return code;
};

Blockly.JavaScript['hms_motion_gotoxy'] = function(block) {
  var text_x_val = block.getFieldValue('x_val');
  var text_y_val = block.getFieldValue('y_val');
  // TODO: Assemble JavaScript into code variable.
  var code = "gotoxy(\"" + text_x_val + "\",\"" + text_y_val + "\");\n";
  return code;
};