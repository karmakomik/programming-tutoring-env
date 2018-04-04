height = 5
size = 4

for x in range(0,height):
  for y in range(0,size):
    for z in range(0,size):  
      haathiObject.placeBlock()
      haathiObject.moveForward()
    haathiObject.turnRight()
  haathiObject.moveUp()

for x in range(0,size + 1):
  haathiObject.moveForward()

for x in range(0,height):
  haathiObject.moveDown()


