#Example of SOAP client
#lufer
#See https://www.geeksforgeeks.org/python/making-soap-api-calls-using-python/
#Check with https://www.w3schools.com/xml/tempconvert.asmx
#Check with C# SOAP Service CalSimples: http://localhost:64019/Services/CalcSimples.asmx?WSDL

import requests;
import xml.etree.ElementTree as ET

url="http://localhost:64019/Services/CalcSimples.asmx"

xValue = 2
yValue = 5

envelop = """<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <Soma xmlns="http://ola.como.estas/">
      <x>{x}</x>
      <y>{y}</y>
    </Soma>
  </soap:Body>
</soap:Envelope>""".format(x=xValue, y=yValue)

options = {
    "Content-Type": "text/xml; charset=utf-8"
}

response = requests.post(url,data=envelop, headers=options)
s=response.text
#check
print ("MSG: " + s)

#XML Parsing 
root = ET.fromstring(s)
#see all
#check
# for child in root.iter("*"):
#    print (child.tag, " - ", child.text)


for child in root.iter("{http://ola.como.estas/}SomaResult"):
    r = child.text
#
print("R: "+r)