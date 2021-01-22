
import cv2
import numpy as np

# Kamera zugriff
widthImg = 480      # an Kamera anpassen
heightImg = 640     # an Kamera anpassen

cap = cv2.VideoCapture(0)  # kamera
cap.set(3, widthImg)
cap.set(4, heightImg)
cap.set(10, 150)
img = cap

imgGray = cv2.cvtColor(img, cv2.COLOR_BGR2Gray) 

while True:

    lower = np.array()
    upper = np.array()
    mask = cv2.inRange(imgGray, )

    cv2.imshow("Video", img)
    cv2.imshow("VideoGray", imgGray)
    cv2.imshow("mask", mask)
    cv2.waitkey(1)
