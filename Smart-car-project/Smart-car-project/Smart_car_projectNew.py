#import motor
import time
import cv2
import numpy as np
import imgStack as stack


#cap = cv2.VideoCapture(0)
#cap.set(3, 640)
#cap.set(4, 480)
#cap.set(10, 100)
#img = cv2.imread("Resources/testPictures67.jpg")
#cap = cv2.VideoCapture("Resources/geradeWWK1.mp4")
cap = cv2.VideoCapture("Resources/greadeMitSpurhalteassistent.mp4")
global width, height, scalingFactorX, scalingFactorY;

scalingFactorX = 0.35
scalingFactorY = 0.35

width = int(scalingFactorX * cap.get(3))
height = int(scalingFactorY * cap.get(4))


#motor.setup()
#car.init(50)


def getContours(img):
    #Fuer den Raspberry, da cv2.findContours 3 Rueckgabewerte hat
    # _, contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    #contours, hierachy = cv2.findContours(img, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    c = max(contours, key=cv2.contourArea)
    area = cv2.contourArea(c)
    cv2.drawContours(imgContour, c, -1, (255, 0, 0), 3)
    if len(contours) > 0:
        M = cv2.moments(c)
        if (M['m00'] != 0):
            cx = int(M['m10']/M['m00'])
            cy = int(M['m01']/M['m00'])
            cv2.line(imgContour,(cx,0),(cx,height),(0,0,255),1)
            cv2.line(imgContour,(0,cy),(width,cy),(0,0,255),1)
             #motor.forward()
            driving(cx)
        else:
            print "stop"
            #   motor.stop()
    else:
        print 'Konturen' + len(contours)





def driving(cx):
    lowerBound = width / 3
    upperBound = width * 2 / 3

    if lowerBound < cx and cx < upperBound:
        print "forward"
       # car.turn(90)
    elif cx >= width * 2 / 3:
        print "right"
       # car.turnRight()
    else:
        print "left"
       # car.turnLeft()

while True:
    success, img = cap.read()
    if (not success):
        break
    img = cv2.resize(img, None, fx=scalingFactorX, fy=scalingFactorY) 
    imgContour = img.copy()
    #GaussianBlur
    imgBlur = cv2.GaussianBlur(img, (5,5), 0)

    #Convert to HSV
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)

    #HSV value of whitish gray
    lower = np.array([17, 6, 131])
    upper = np.array([45, 35, 255])

    #Mask 
    mask = cv2.inRange(imgHSV, lower, upper)
    
    #Canny Edge Detection
    imgCanny = cv2.Canny(mask, 50, 150)

    kernel = np.ones((5,5))
    imgDil = cv2.dilate(imgCanny, kernel, 2)
    getContours(imgDil)

    #Fasst alle Images zu einem zusammen
    imgStack = stack.stackImages(0.6,([img, imgHSV, mask],[imgCanny, imgDil, imgContour]))
    #cv2.imshow('mask', mask)
    #cv2.imshow('HSV', imgHSV)
    #cv2.imshow('imgCanny', imgCanny)

    cv2.imshow("Pic0", imgStack)
    #cv2.waitKey(0)
    if (cv2.waitKey(1) & 0xFF == ord('q')):
        break


cap.release()


cv2.destroyAllWindows()
#motor.stop()

#for raspberry pi camera
#for frame in camera.capture_continuous(rawCapture, format="bgr", use_video_port=True):
#    # grab the raw NumPy array representing the image, then initialize the timestamp
#    # and occupied/unoccupied text
#    img = frame.array
#    imgContour = img.copy()
#    imgBlur = cv2.GaussianBlur(img, (7,7), 2) 
#    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)
#    lower = np.array([0, 0, 0])
#    upper = np.array([0, 0, 255])
#    mask = cv2.inRange(imgHSV, lower, upper)
#    imgCanny = cv2.Canny(mask, 200, 250, 5)
#    kernel = np.ones((5,5))
#    imgDil = cv2.dilate(imgCanny, kernel, 2)
#    getContours(imgDil)
#    imgStack = stack.stackImages(0.6,([img, imgContour, imgHSV],[mask, imgCanny, imgDil]))
#    # clear the stream in preparation for the next frame
#    rawCapture.truncate(0)
#
# 
#
#camera.close()