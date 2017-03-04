/**
 * @fileoverview Generating JavaScript for Haathi Mera Saathi blocks.
 * @author unni.r.krishnan@gmail.com (Unnikrishnan R)
 */
'use strict';

goog.provide('Blockly.JavaScript.hms_pen');

goog.require('Blockly.JavaScript');

Blockly.JavaScript['hms_pen_down'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'penDown();\n';
  return code;
};

Blockly.JavaScript['hms_pen_up'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'penUp();\n';
  return code;
};

Blockly.JavaScript['hms_pen_clear'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'penClear();\n';
  return code;
};

Blockly.JavaScript['hms_pen_set_color'] = function(block) {
  var colour_colorid = block.getFieldValue('colorID');
  // TODO: Assemble JavaScript into code variable.
  var code = "penSetColor(\"" + colour_colorid + "\");\n";
  return code;
};

Blockly.JavaScript['hms_pen_set_size'] = function(block) 
{
  var num_penSize = block.getFieldValue('penSize');  
  // TODO: Assemble JavaScript into code variable.
  var code = "penSetSize(\"" + num_penSize + "\");\n";  
  return code;
};