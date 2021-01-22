

import cv2
import numpy as np

widthImg = 480      # an Kamera anpassen
heightImg = 640     # an Kamera anpassen

cap = cv2.VideoCapture(0)  # kamera
cap.set(3, widthImg)
cap.set(4, heightImg)
cap.set(10, 150)


#def preProcessing(img):
#    imgGray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)    # bild grau
#    imgBlur = cv2.GaussianBlur(imgGray, (5,5), 1)       # bild verschwommen
#    imgCanny = cv2.Canny(imgBlur, 200, 200)             # bild kanten dreutlicher
#    kernel = np.ones((5,5))                                             # sicherere erkennung
#    imgDial = cv2.dilate(imgCanny, kernel, iterations=2) # dicker       # sicherere erkennung
#    imgThres = cv2.erode(imgDial, kernel, iterations = 1) # duenner      # sicherere erkennung
#    return imgThres
#
#
#def getContours(img):
#    biggest = np.array([])
#    maxArea = 0
#    contours, hierachy = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_NONE)
#    for cnt in contours:
#        area = cv2.contourArea(cnt)
#        if area > 5000:
#            # cv2.drawContours(imgContour, cnt, -1, (255, 0, 0), 3)   # kanten zeichnen
#            peri = cv2.arcLength(cnt, True)
#            approx = cv2.approxPolyDP(cnt, 0.02*peri, True)
#            if area > maxArea and len(approx) == 4:
#                biggest = approx
#                maxArea = area
##    cv2.drawContours(imgContour, biggest, -1, (255, 0, 0), 3)         # eckpunkte zeichnen
#    return biggest
#
#
##def getWarp(img, biggest):
##    2:33
#
#
while True:
    success, img = cap.read()
#    # img = cv2.resize(widthImg, heightImg)
##    imgContour = img.Copy()
#
#    imgThres = preProcessing(img)
#    biggest = getContours(imgThres)
#
#    imgWarped = getWarp(img, biggest)
#
    cv2.imshow("result", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

