#import motor

import time
import cv2
import numpy as np
import trajectory 
import imgStack as stack
#import car


#cap = cv2.VideoCapture(0)
#cap.set(3, 640)
#cap.set(4, 480)
#cap.set(10, 100)
#img = cv2.imread("Resources/testPictures2.0.jpg")
cap = cv2.VideoCapture("Resources/kurveRechtsNK.avi")


height =480 # NK = 480; WWK = 1080
width = 640 # NK = 640; WWK = 1920

#motor.setup()
#car.init(50)



def getContours(img):
    contours, hierachy = cv2.findContours(img, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE) ### zum testen am Rechner2

    #contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE) ### zum testen am Rechner1
    # _, contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE) ### fuer Raspberry
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
                cv2.line(imgContour,(cx,0),(cx,height),(0,0,255),1)
                cv2.line(imgContour,(0,cy),(width,cy),(0,0,255),1)
            #    motor.forward()
                driving(cx)
            else:
                print "stop"
             #   motor.stop()
        else:
            print 'Konturen' + len(contours)




def driving(cx):
    if cx > width/3 and cx < width * 2/3:
        print "forward"
       # car.turn(90)
    elif cx >= width * 2/3:
        print "right"
       # car.turnRight()
    else:
        print "left"
       # car.turnLeft()



while True:
    success, img = cap.read()
    #imgCrop = img[int(height / 2.8) : height, 0 : width]  # for WWK (take care of the next row)
    imgContour = img.copy()
    imgBlur = cv2.GaussianBlur(imgContour, (7,7), 2) 
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)
    lower = np.array([0, 0, 150])
    upper = np.array([0, 0, 255])
    mask = cv2.inRange(imgHSV, lower, upper)
    imgCanny = cv2.Canny(mask, 200, 250, 5)
    kernel = np.ones((5,5))
    imgDil = cv2.dilate(imgCanny, kernel, 2)
    getContours(imgDil)
    imgStack = stack.stackImages(0.6,([img, imgContour, imgHSV],[mask, imgCanny, imgDil]))
    #cv2.imshow("Pic0", imgContour)
    #cv2.imshow("Pic1", imgDil)
    cv2.imshow("Pic0", imgStack)
#cv2.waitKey(0)
    if (cv2.waitKey(1) & 0xFF == ord('q')):
        break


cap.release()
cv2.destroyAllWindows()
#motor.stop()
