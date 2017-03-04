/**
 * @fileoverview Generating JavaScript for Haathi Mera Saathi blocks.
 * @author unni.r.krishnan@gmail.com (Unnikrishnan R)
 */
'use strict';

goog.provide('Blockly.JavaScript.hms_looks');

goog.require('Blockly.JavaScript');

Blockly.JavaScript['hms_looks_say'] = function(block) 
{
  var text_saymsg = block.getFieldValue('sayMsg');  
  // TODO: Assemble JavaScript into code variable.
  var code = "say(\"" + text_saymsg + "\");\n";
  //var code = "say();\n";  
  return code;
};

Blockly.JavaScript['hms_looks_say_for_time'] = function(block) {
  var text_saymsg = block.getFieldValue('sayMsg');
  var text_time = block.getFieldValue('time');
  // TODO: Assemble JavaScript into code variable.
  var code = "sayForTime(\"" + text_saymsg + "\",\"" + text_time + "\");\n";  
  return code;
};

Blockly.JavaScript['hms_looks_think'] = function(block) 
{
  var text_saymsg = block.getFieldValue('sayMsg');  
  // TODO: Assemble JavaScript into code variable.
  var code = "think(\"" + text_saymsg + "\");\n";  
  return code;
};

Blockly.JavaScript['hms_looks_think_for_time'] = function(block) 
{
  var text_saymsg = block.getFieldValue('sayMsg');
  var text_time = block.getFieldValue('time');
  // TODO: Assemble JavaScript into code variable.
  var code = "thinkForTime(\"" + text_saymsg + "\",\"" + text_time + "\");\n";
  return code;
};

Blockly.JavaScript['hms_looks_change_costume'] = function(block) 
{
  var dropdown_costumeid = block.getFieldValue('costumeID');
  // TODO: Assemble JavaScript into code variable.
  var code = "changeCostume(\"" + dropdown_costumeid + "\");\n"; 
  return code;
};

Blockly.JavaScript['hms_looks_show'] = function(block) 
{
  // TODO: Assemble JavaScript into code variable.
  var code = 'lookShow();\n';
  return code;
};

Blockly.JavaScript['hms_looks_hide'] = function(block) 
{
  // TODO: Assemble JavaScript into code variable.
  var code = 'lookHide();\n';
  return code;
};