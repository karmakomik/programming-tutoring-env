for x in range(0,4):
  for y in range(0,8):
    haathiObject.wait(0.3)
    haathiObject.placeBlock()
  if x%2 == 0:
    haathiObject.turnRight()
  else:
    haathiObject.turnLeft()