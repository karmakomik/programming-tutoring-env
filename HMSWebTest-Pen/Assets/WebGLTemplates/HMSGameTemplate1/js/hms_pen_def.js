/**
* Author : unni.r.krishnan@gmail.com (Unnikrishnan R), Ammachi labs
* Date   : 8th August 2016
* This file contains the definitions of the functions defined in hms_pen.js in the google-blockly
*/

function penUp()
{
    console.log("pen up");
    SendMessage("BrowserConnect", "addCommand", "penUp");
}

function penDown()
{
    console.log("pen down");
    SendMessage("BrowserConnect", "addCommand", "penDown");
}

function penClear()
{
    console.log("pen clear");
    SendMessage("BrowserConnect", "addCommand", "penClear");
}

function penSetColor(colorID)
{
    console.log("Pen color : " + colorID);
    SendMessage("BrowserConnect", "addCommand", "penSetColor " + colorID);
}
