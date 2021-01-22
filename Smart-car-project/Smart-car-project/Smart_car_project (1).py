#import motor
import time
#import car_dir
import cv2
import numpy as np
import trajectory 
import imgStack as stack


#cap = cv2.VideoCapture(0)
#cap.set(3, 640)
#cap.set(4, 480)
#cap.set(10, 100)
#img = cv2.imread("Resources/testPictures67.jpg")
cap = cv2.VideoCapture("Resources/geradeWWK1.mp4")
#cap = cv2.VideoCapture("Resources/kurveLinksWWK1.mp4")
#cap = cv2.VideoCapture("Resources/geradeGut.mp4")
#cap = cv2.VideoCapture("Resources/geradeMitSpurhalteassistent.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenabenSChlecht.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenAberbesser.mp4")
#cap = cv2.VideoCapture("Resources/kurveRechtsWWK2.mp4")
#cap = cv2.VideoCapture("Resources/geradeNK1.avi")

scalingFactorX = 0.35
scalingFactorY = 0.35
width = int(scalingFactorX * cap.get(3))
height = int(scalingFactorY * cap.get(4))
heightCrop = height * 3/4
#motor.setup()
#car.init(50)

def getContours(img):
    #Fuer den Raspberry, da cv2.findContours 3 Rueckgabewerte hat
    # _, contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    if len(contours) > 0:
        c = max(contours, key=cv2.contourArea)
        cv2.drawContours(imgContour, c, -1, (255, 0, 0), 4)
        M = cv2.moments(c)
        if (M['m00'] != 0):
            cx = int(M['m10']/M['m00'])
            cy = int(M['m01']/M['m00'])
            cv2.line(imgContour,(cx,0),(cx,heightCrop),(0,0,255),1)
            cv2.line(imgContour,(0,cy),(width,cy),(0,0,255),1)

            cv2.line(imgContour,(int(width/2), heightCrop),(cx,cy),(0,255,0),1)
            cv2.line(imgContour,(int(width/2), cy),(int(width/2), heightCrop),(0,255,0),1)
            cv2.line(imgContour,(cx,cy),(int(width/2), cy),(0,255,0),1)
            #angle(cx,cy)
        #    motor.forward()
            driving(cx, cy)
        else:
            print "stop"
         #   motor.stop()
# links = 3,03
# rechts = 2,38
def angle(cx,cy):
    ank = heightCrop - cy
    ggk = int(width/2 - cx)
    if ggk < 0:
        ggk *= -1
    elif ggk == 0:
        #car_dir.home()
        print 'exakt gerade aus'
        return
    hpn = np.sqrt(ggk**2 + (ank)**2)
    sinRad = np.sin(ggk/hpn)
    steerAngle = sinRad*180/np.math.pi
    print sinRad 
    print("cx,cy",(cx,cy),"w,h",width,284, "last 4",ank,ggk,hpn, steerAngle)
    if (width/2 - cx) < 0:
    #    car_dir.turn_right_small(steerAngle)
        print 'fahre nach rechts ' + str(steerAngle) 
    else:
    #    car_dir.turn_left_small(-steerAngle)
        print 'fahre nach links ' + str(-steerAngle)

def driving(cx, cy):
    lowerBound = width / 3
    upperBound = width * 2 / 3

    if lowerBound < cx and cx < upperBound:
        angle(cx,cy)
        print "home"
       # car_dir.home()
    elif cx >= upperBound:
        print "right"
       # car_dir.turn_right()
    else:
        print "left"
       # car_dir.turn_left()

while True:
    success, img = cap.read()
    if (not success):
        break
    img = cv2.resize(img, None, fx=scalingFactorX, fy=scalingFactorY)
    img = img[int(height / 4) : height, 0 : width]

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
    #imgStack = stack.stackImages(0.6,([img, imgHSV, mask],[imgCanny, imgDil, imgContour]))
    imgStackContours = stack.stackImages(0.6, ([img, imgHSV, mask], [imgDil, imgContour, imgCanny]))
    #cv2.imshow('mask', mask)
    #cv2.imshow('HSV', imgHSV)
    #cv2.imshow('imgCanny', imgCanny)

    #cv2.imshow("Pic0", imgStack)
    cv2.imshow("Pic0", imgStackContours)
    #cv2.waitKey(0)
    if (cv2.waitKey(1) & 0xFF == ord('q')):
        break


cap.release()


cv2.destroyAllWindows()
