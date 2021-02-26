import time
import cv2
import numpy as np

#cap = cv2.VideoCapture("Resources/geradeWWK1.mp4")
#cap = cv2.VideoCapture("Resources/kurveLinksWWK1.mp4")
cap = cv2.VideoCapture("Resources/gerade_andersrum.mp4")
#cap = cv2.VideoCapture("Resources/geradeGut.mp4")
#cap = cv2.VideoCapture("Resources/geradeMitSpurhalteassistent.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenabenSChlecht.mp4")
#cap = cv2.VideoCapture("Resources/geradeSpurHaltenAberbesser.mp4")

def empty(a):
    pass
#Best default values so far
#(0, 105, 0, 98, 158, 255) gutes vid 
#(0, 106, 0, 58, 149, 255) gutes Vid
#(95, 105, 26, 78, 93, 182) beim schlechten vid


# (0, 179, 0, 45, 205, 255) gerade andersrum gut



#erster Versuch
#Hue Min 17
#Hue Max 76
#Sat Min 7
#Sat Max 255
#Val Min 131
#Val Max 255
#
#zweiter Versuch
#Hue Min 16
#Hue Max 45
#Sat Min 7
#Sat Max 35
#Val Min 131
#Val Max 255

cv2.namedWindow("Trackbars")
cv2.resizeWindow("Trackbars", 640,240)
cv2.createTrackbar("Hue Min", "Trackbars", 17,179, empty)
cv2.createTrackbar("Hue Max", "Trackbars", 76,179, empty)
cv2.createTrackbar("Sat Min", "Trackbars", 6,255, empty)
cv2.createTrackbar("Sat Max", "Trackbars", 35,255, empty)
cv2.createTrackbar("Val Min", "Trackbars", 131,255, empty)
cv2.createTrackbar("Val Max", "Trackbars", 255,255, empty)

breakLoop = False

scalingFactorX = 0.35
scalingFactorY = 0.35

width = int(scalingFactorX * cap.get(3))
height = int(scalingFactorY * cap.get(4))

while not breakLoop:
    success, img = cap.read()
    if (not success):
        break
    
    breakLoop = False

    img = cv2.resize(img, None, fx=scalingFactorX, fy=scalingFactorY)
    img = img[int(height / 4) : height, 0 : width]
    #GaussianBlur
    imgBlur = cv2.GaussianBlur(img, (7,7), 0)
    #Convert to HSV
    imgHSV = cv2.cvtColor(imgBlur, cv2.COLOR_BGR2HSV)
    #Show images
    cv2.imshow('Original', img)
    cv2.imshow('HSV', imgHSV)

    while True:
        h_min = cv2.getTrackbarPos("Hue Min", "Trackbars")
        h_max = cv2.getTrackbarPos("Hue Max", "Trackbars")
        s_min = cv2.getTrackbarPos("Sat Min", "Trackbars")
        s_max = cv2.getTrackbarPos("Sat Max", "Trackbars")
        v_min = cv2.getTrackbarPos("Val Min", "Trackbars")
        v_max = cv2.getTrackbarPos("Val Max", "Trackbars")
        print(h_min, h_max, s_min, s_max, v_min, v_max)

        lower = np.array([h_min,s_min,v_min])
        upper = np.array([h_max,s_max,v_max])

        mask = cv2.inRange(imgHSV,lower,upper)

        #wwcv2.waitKey(0)
        cv2.imshow('Mask', mask)

        keyPress = cv2.waitKey(1)
        if keyPress & 0xFF == ord('q'):
            breakLoop = True
            break
        elif (keyPress & 0xFF == ord('w')):
            break

    a = 12
     
cv2.destroyAllWindows()
