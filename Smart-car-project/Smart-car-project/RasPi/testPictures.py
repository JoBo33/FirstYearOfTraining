
import cv2

#img = cv2.imread("Resources/testPictures1.jpg")
#cv2.imshow("Pic", img)
#cv2.waitKey(0)
cap = cv2.VideoCapture(0)
cap.set(3, 640)
cap.set(4, 480)
cap.set(10, 100)


currentframe = 0
count = 0
while True:
    sucess, img = cap.read()
    count+=1
    name = r'testPictures' + str(currentframe) + '.jpg'
    cv2.imshow("vid", img)
    if count == 50:
        cv2.imwrite(name,  img)
        currentframe +=1
        print name
        count = 0

    if (cv2.waitKey(1) & 0xFF == ord('q')):
        break
