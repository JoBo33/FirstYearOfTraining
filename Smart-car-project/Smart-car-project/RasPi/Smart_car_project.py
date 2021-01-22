import motor
import time
import cv2
import numpy as np
import trajectory 
import imgStack as stack
import car


cap = cv2.VideoCapture(0)
cap.set(3, 640)
cap.set(4, 480)
cap.set(10, 100)
#img = cv2.imread("Resources/testPictures1.jpg")

motor.setup()
car.init(50)
global motorActive
motorActive = false
def getContours(img):
    print 'a'
    _, contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    for cnt in contours:
        area = cv2.contourArea(cnt)
        if area > 500:
            cv2.drawContours(imgContour, cnt, -1, (255, 0, 0), 3)
        if len(contours) > 0:
            c = max(contours, key=cv2.contourArea)
            M = cv2.moments(c)
            print 'b'
            if (M['m00'] != 0):
		print 'c'
                cx = int(M['m10']/M['m00'])
                cy = int(M['m01']/M['m00'])
                cv2.line(imgContour,(cx,0),(cx,720),(0,0,255),1)
                cv2.line(imgContour,(0,cy),(1280,cy),(0,0,255),1)
                if motorActive == False:
		    motorActive = True
		    motor.forward()
                #cv2.point(imgContour,(cx,cy),(0,255,255),1)
                #cv2.drawContours(imgContour, contours, 0, (0,255,0), 1)
                    driving(cx)
            else:
		motorActive = False
                print "stop"
                motor.stop()
def driving(cx):
    if cx >200 and cx < 440:
        print "forward"
        car.turn(90)
    elif cx>439:
        print "right"
        car.turnRight()
    else:
        print "left"
        car.turnLeft()


#while True:
for x in range(200):
    print x
    success, img = cap.read()
    imgContour = img.copy()
    imgBlur = cv2.GaussianBlur(img, (7,7), 2) 
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)
    lower = np.array([0, 0, 0])
    upper = np.array([200, 100, 255])
    mask = cv2.inRange(imgHSV, lower, upper)
    imgCanny = cv2.Canny(mask, 200, 250, 5)
    kernel = np.ones((5,5))
    imgDil = cv2.dilate(imgCanny, kernel, 2)
    getContours(imgDil)
    imgStack = stack.stackImages(0.6,([img, imgContour, imgHSV],[mask, imgCanny, imgDil]))
    #cv2.imshow("Pic0", imgStack)
    #if (cv2.waitKey(1) & 0xFF == ord('q')):
    #    break

#cap.release()
#cv2.destroyAllWindows()
motor.stop()
#motor.setup()

 

#pixels = 640 * 480
#
# 
#
#motorActive = False
#
# 
#while True:
#    sucess, img = cap.read()
#    imgGray = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
#    counter = np.sum(imgGray > 130)
#    #print counter
#    #(thresh, im_bw) = cv2.threshold(imgGray, 128, 255, cv2.THRESH_BINARY | cv2.THRESH_OTSU)
#    whitePixels = float(counter) / float(pixels) > 0.90
#    print 'WhitePixels ' + str(whitePixels)
#    number = trajectory.direction_to_take(imgGray)
#    print number
#
#    if whitePixels:
#        if(motorActive == False):
##            motor.forwardWithSpeed(50)
#            print counter
#            print 'motorForward'
#        motorActive = True
#    else:
#        if(motorActive == True):
##            motor.stop()
#            print counter
#            print 'motorStop'
#        motorActive = False
#
# 
#
#    cv2.imshow("Video", img)
#    #cv2.imshow("Video", im_bw)
#
# 
#
#    if (cv2.waitKey(1) & 0xFF == ord('q')):
#        break
