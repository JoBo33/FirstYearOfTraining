#import motor
import time
import cv2
import numpy as np
import trajectory 
import imgStack as stack
#import car
from picamera.array import PiRGBArray 
from picamera import PiCamera

# initialize the camera and grab a reference to the raw camera capture
camera = PiCamera()
camera.resolution = (1920, 1088)
camera.framerate = 32
rawCapture = PiRGBArray(camera, size=(1920, 1088))

 
# allow the camera to warmup
time.sleep(0.1)


#motor.setup()
#car.init(50)


def getContours(img):
    contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    for cnt in contours:
        area = cv2.contourArea(cnt)
        if area > 3000:
            cv2.drawContours(imgContour, cnt, -1, (255, 0, 0), 3)
        if len(contours) > 0:
            c = max(contours, key=cv2.contourArea)
            M = cv2.moments(c)
            if (M['m00'] != 0):
                cx = int(M['m10']/M['m00'])
                cy = int(M['m01']/M['m00'])
                cv2.line(imgContour,(cx,0),(cx,720),(0,0,255),1)
                cv2.line(imgContour,(0,cy),(1280,cy),(0,0,255),1)
            #    motor.forward()
                print 'go'
                driving(cx)
            else:
                print "stop"
             #   motor.stop()
        else:
            print 'Konturen' + len(contours)




def driving(cx):
    if cx >250 and cx < 390:
        print "forward"
       # car.turn(90)
    elif cx>389:
        print "right"
       # car.turnRight()
    else:
        print "left"
       # car.turnLeft()

#motor.stop()

for frame in camera.capture_continuous(rawCapture, format="bgr", use_video_port=True):
    # grab the raw NumPy array representing the image, then initialize the timestamp
    # and occupied/unoccupied text
    img = frame.array
    imgContour = img.copy()
    imgBlur = cv2.GaussianBlur(img, (7,7), 2) 
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)
    lower = np.array([0, 0, 0])
    upper = np.array([0, 0, 255])
    mask = cv2.inRange(imgHSV, lower, upper)
    imgCanny = cv2.Canny(mask, 200, 250, 5)
    kernel = np.ones((5,5))
    imgDil = cv2.dilate(imgCanny, kernel, 2)
    getContours(imgDil)
    imgStack = stack.stackImages(0.6,([img, imgContour, imgHSV],[mask, imgCanny, imgDil]))
    # clear the stream in preparation for the next frame
    rawCapture.truncate(0)

 

camera.close()
