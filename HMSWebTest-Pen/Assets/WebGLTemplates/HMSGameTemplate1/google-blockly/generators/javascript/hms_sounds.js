/**
 * @fileoverview Generating JavaScript for Haathi Mera Saathi blocks.
 * @author unni.r.krishnan@gmail.com (Unnikrishnan R)
 */
'use strict';

goog.provide('Blockly.JavaScript.hms_sounds');

goog.require('Blockly.JavaScript');

Blockly.JavaScript['hms_sounds_play'] = function(block) {
  var dropdown_soundid = block.getFieldValue('soundID');
  // TODO: Assemble JavaScript into code variable.
  var code = "playSound(\"" + dropdown_soundid + "\");\n";
  return code;
};