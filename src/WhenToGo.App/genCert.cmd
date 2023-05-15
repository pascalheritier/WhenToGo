@echo Off
cd /d "%~dp0"
del android-key.pfx
keytool.exe -genkey -v -alias key -keyalg RSA -keysize 2048 -validity 10000 -keystore android-key.pfx -keypass pa68WPD6uHemsD3xWj3j -storepass pa68WPD6uHemsD3xWj3j -dname "CN=ca-pascalheritier, O=Pascal Heritier, L=Preverenges, S=Vaud, C=CH"