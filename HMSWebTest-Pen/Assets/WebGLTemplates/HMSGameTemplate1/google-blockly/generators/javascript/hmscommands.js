/**
 * @fileoverview Generating JavaScript for Haathi Mera Saathi blocks.
 * @author unni.r.krishnan@gmail.com (Unnikrishnan R)
 */
'use strict';

goog.provide('Blockly.JavaScript.hmscommands');

goog.require('Blockly.JavaScript');

Blockly.JavaScript['moveforward'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'moveForward();\n';
  return code;
};

Blockly.JavaScript['turnleft'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'turnLeft();\n';
  return code;
};

Blockly.JavaScript['turnright'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'turnRight();\n';
  return code;
};

Blockly.JavaScript['eat'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'eat();\n';
  return code;
};