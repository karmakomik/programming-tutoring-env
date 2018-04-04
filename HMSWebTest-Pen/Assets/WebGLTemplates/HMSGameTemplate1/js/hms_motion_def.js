/**
* Author : unni.r.krishnan@gmail.com (Unnikrishnan R), Ammachi labs
* Date   : 8th August 2016
* This file contains the definitions of the functions defined in hms_motion.js in the google-blockly
*/

function moveForward(text_numsteps)
{
    console.log("blockly : moveForward");
    SendMessage("BrowserConnect", "addCommand", "moveForward " + text_numsteps);
}

function turnLeft()
{
    console.log("blockly : turnLeft");
    //SendMessage("BrowserConnect", "addCommand", "turnLeft");
}

function turnRight()
{
    console.log("blockly : turnRight");
    //SendMessage("BrowserConnect", "addCommand", "turnRight");
}

function eat()
{
    console.log("blockly : eat");
   // SendMessage("BrowserConnect", "addCommand", "eat");
}

function turnClockwise(angle_ang)
{
    console.log("turnClockwise : " + angle_ang);
    SendMessage("BrowserConnect", "addCommand", "turnClockwise " + angle_ang);
}

function turnAntiClockwise(angle_ang)
{
    console.log("turnAntiClockwise : " + angle_ang);
    SendMessage("BrowserConnect", "addCommand", "turnAntiClockwise " + angle_ang);
}

function gotoxy(text_x_val, text_y_val)
{
    console.log("gotoxy " + text_x_val + " " + text_y_val );
}
