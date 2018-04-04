haathiObject.setBlockColor(Color.red)
for y in range(0,4):
  for x in range(0,3):
    haathiObject.placeBlock()
    haathiObject.moveForward()
    haathiObject.placeBlock()
    haathiObject.moveForward()
    haathiObject.moveUp()
  haathiObject.turnRight()

