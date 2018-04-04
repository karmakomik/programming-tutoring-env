/**
* Author : unni.r.krishnan@gmail.com (Unnikrishnan R), Ammachi labs
* Date   : 8th August 2016
* This file contains the definitions of the functions defined in hms_looks.js in the google-blockly
*/

function say(text_saymsg)
{
    console.log("say " + text_saymsg);
}

function sayForTime(text_saymsg, text_time)
{
    console.log("sayForTime " + text_saymsg + " " + text_time );
}

function think(text_saymsg)
{
    console.log("think " + text_saymsg);
}

function thinkForTime(text_saymsg, text_time)
{
    console.log("thinkForTime " + text_saymsg + " " + text_time );
}

function changeCostume(dropdown_costumeid)
{
    console.log("changeCostume " + dropdown_costumeid);
}

function lookShow()
{
    console.log("show");
    SendMessage("BrowserConnect", "addCommand", "show");
}

function lookHide()
{
    console.log("hide");
    SendMessage("BrowserConnect", "addCommand", "hide");
}