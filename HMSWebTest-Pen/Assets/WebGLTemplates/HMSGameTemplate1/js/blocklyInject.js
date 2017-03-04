/**
* Author : unni.r.krishnan@gmail.com (Unnikrishnan R), Ammachi labs
* Date   : 30th June 2016
* This file contains code that inserts google-blockly into the main HTML page and also handles communication with Unity3D
*/

var workspace = Blockly.inject('blocklyDiv', {toolbox: document.getElementById('toolbox')});

var currCode = ""; 
//document.getElementById("blocklyToolboxDiv").style.opacity = 0.0; 

function evalCode() 
{
    // Generate JavaScript code and display it.
    //Blockly.JavaScript.INFINITE_LOOP_TRAP = null;
    
    currCode = Blockly.JavaScript.workspaceToCode(workspace);
    //alert(currCode);
    try
    { 
        eval(currCode);
        SendMessage("BrowserConnect", "startExecution"); //Message to Unity 
        //document.getElementById("blocklyDiv").style.opacity=0.2;  
    } 
    catch (e) 
    {
        alert(e);
    }
}

function startupOps()
{
    console.log("startup");
    $(".blocklyToolboxDiv").hide();
    document.getElementById("blocklyDiv").style.opacity = 0.0;
    //workspace.addChangeListener(Blockly.Events.disableOrphans);
    /*Blockly.makeColour = function(hue) {
      return "#ff0000";
    };*/
}

function setVisibility(val)
{
    if(val == 1)
    {
        document.getElementById("blocklyDiv").style.opacity = 0.0;
        $(".blocklyToolboxDiv").hide();
    }
    else
    {
        document.getElementById("blocklyDiv").style.opacity = 1.0;
        $(".blocklyToolboxDiv").show();       
    }  
}