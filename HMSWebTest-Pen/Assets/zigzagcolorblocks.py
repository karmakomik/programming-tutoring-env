haathiObject.setBlockColor(Color.red)
for x in range(0,4):
  for y in range(0,8):
    haathiObject.wait(0.3)
    haathiObject.placeBlock()
    haathiObject.moveForward()
  if x%2 == 0:
    haathiObject.turnRight()
    haathiObject.setBlockColor(Color.blue)
  else:
    haathiObject.turnLeft()
    haathiObject.setBlockColor(Color.green)