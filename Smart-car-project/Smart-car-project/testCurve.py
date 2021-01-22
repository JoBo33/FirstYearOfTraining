import time
import car
import motor
import cv2


#cap = cv2.VideoCapture(0)
#cap.set(3, 640)
#cap.set(4, 480)
#cap.set(10, 100)

#motor.setup()
car.init(50)

#motor.forwardWithSpeed(0)

car.turnLeft()
time.sleep(3)

#motor.stop()