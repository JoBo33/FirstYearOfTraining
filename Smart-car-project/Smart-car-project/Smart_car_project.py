#import motor
import time
#import car_dir
import cv2
import numpy as np
import imgStack as stack


#cap = cv2.VideoCapture(0)
#cap.set(3, 640)
#cap.set(4, 480)
#cap.set(10, 100)
#img = cv2.imread("Resources/testPictures67.jpg")
#cap = cv2.VideoCapture("Resources/geradeWWK1.mp4")
#cap = cv2.VideoCapture("Resources/kurveLinksWWK1.mp4")
#cap = cv2.VideoCapture("Resources/gerade_andersrum.mp4")
#cap = cv2.VideoCapture("Resources/geradeMitSpurhalteassistent.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenabenSChlecht.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenAberbesser.mp4")
cap = cv2.VideoCapture("Resources/kurveRechtsWWK2.mp4")
#cap = cv2.VideoCapture("Resources/geradeNK1.avi")

scalingFactorX = 0.35
scalingFactorY = 0.35
width = int(scalingFactorX * cap.get(3))
height = int(scalingFactorY * cap.get(4))
cropFactorY = 1.0/4
heightCrop = int(height * (1- cropFactorY))
threshold = 2
#motor.setup()
#car.init(50)


### 
# GetContours finds every Countour on an image but works just with the biggest one.
# By means of moments moments the center of the contour will be calculated. 
###

def getContours(img):
    #For the Raspberry, because cv2.findContours has 3 return values
    # _, contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
    if len(contours) > 0:
        # calculate center of the biggest contour
        c = max(contours, key=cv2.contourArea)
        cv2.drawContours(imgContour, c, -1, (255, 0, 0), 4)
        M = cv2.moments(c)
        if (M['m00'] != 0):
            cx = int(M['m10']/M['m00'])
            cy = int(M['m01']/M['m00'])
            # draw centered lines
            cv2.line(imgContour,(cx,0),(cx,heightCrop),(0,0,255),1)
            cv2.line(imgContour,(0,cy),(width,cy),(0,0,255),1)

            # draw triangle
            cv2.line(imgContour,(int(width/2), heightCrop),(cx,cy),(0,255,0),1)
            cv2.line(imgContour,(width/2, cy),(width/2, heightCrop),(0,255,0),1)
            cv2.line(imgContour,(cx,cy),(int(width/2), cy),(0,255,0),1)

        #    motor.forward()
            driving(angle(cx, cy))
        else:
            print "stop"
            #   motor.stop()



###
# Driving gets an angle. Depending on the value of angle will be decided in wich direction the car should drive.
###

def driving (angle):

    if angle < -30:
        #car_dir.turn_left()
        print 'left max'
    elif angle < 0:
        #car_dir.turn_left_small(angle)
        print 'left' + str(-angle)
    elif angle == 0:
        #car_dir.home()
        print 'home'
    elif angle < 30:
        #car_dir.turn_right_small(angle)
        print 'right' + str(angle)
    else:
        #car_dir.turn_right()
        print 'right max'




###
# angle gets the center point of the biggest contour. 
# The method forms a triangle with following points: (cx,cy) -> center, (width/2, heightCrop) -> camera,  (width/2, cy) -> third Point to get a right triangle
###
lastAngle = 0

def angle(cx,cy):
    global lastAngle
    ank = heightCrop - cy
    ggk = width/2 - cx
    if ggk < 0:
        ggk *= -1
    elif ggk == 0:
        #car_dir.home()
        print 'exactly straight ahead'
        lastAngle = 0
        return lastAngle
    hpn = np.sqrt(ggk**2 + (ank)**2)
    sinRad = np.arcsin(ggk/hpn)
    steerAngle = sinRad*180/np.math.pi

    if (width/2 - cx) < 0 and steerAngle > np.abs(lastAngle) + threshold or steerAngle < np.abs(lastAngle) - threshold:
        lastAngle = steerAngle
        return steerAngle
    elif(width/2 - cx) > 0 and steerAngle > np.abs(lastAngle) + threshold or steerAngle < np.abs(lastAngle) - threshold:
        lastAngle = -steerAngle
        return -steerAngle
    return lastAngle      


while True:
    success, img = cap.read()
    if (not success):
        break
    #scale the image
    img = cv2.resize(img, None, fx=scalingFactorX, fy=scalingFactorY)

    #Crop the image
    img = img[int(height * cropFactorY) : height, 0 : width]     

    imgContour = img.copy()
    #GaussianBlur
    imgBlur = cv2.GaussianBlur(img, (5,5), 0)

    #Convert to HSV
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)

    #HSV value of whitish gray
    lower = np.array([0, 0, 205])      #([17, 6, 131])   detect the right values with LaneDetectionCalibration.py
    upper = np.array([179 , 45, 255])  #([45, 35, 255])

    #Mask 
    mask = cv2.inRange(imgHSV, lower, upper)
    
    #Canny Edge Detection
    imgCanny = cv2.Canny(mask, 50, 150)

    #Makes cotours thicker
    kernel = np.ones((5,5))
    imgDil = cv2.dilate(imgCanny, kernel, 2)
    getContours(imgDil)

    #Combines all images to one
    #imgStack = stack.stackImages(0.6,([img, imgHSV, mask],[imgCanny, imgDil, imgContour]))
    imgStackContours = stack.stackImages(0.6, ([img, imgHSV, mask], [imgDil, imgContour, imgCanny]))

    #cv2.imshow("Pic0", imgStack)
    cv2.imshow("Pic0", imgStackContours)

    #press 'q' to break the video
    if (cv2.waitKey(1) & 0xFF == ord('q')):
        break
    

cap.release()
cv2.destroyAllWindows()
