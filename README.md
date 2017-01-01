# Manager Litterature [![Official Site](https://img.shields.io/badge/site-servodroid.com-orange.svg)](http://servodroid.com)

Parsing all sentenses with vocabulary and times. XML file with almost french words. You can add your own database to have your words.

[![Version Status](https://img.shields.io/nuget/v/Droid_Litterature.svg)](https://www.nuget.org/packages/Droid_Litterature/)
[![License](https://img.shields.io/github/license/brandondahler/Data.HashFunction.svg)](https://raw.githubusercontent.com/ThibaultMontaufray/Tools4Libraries/master/License)
[![Build Status](https://travis-ci.org/ThibaultMontaufray/Manager-Litterature.svg?branch=master)](https://travis-ci.org/ThibaultMontaufray/Manager-Litterature) 
[![Build status](https://ci.appveyor.com/api/projects/status/grb4bad41rlq5upv?svg=true)](https://ci.appveyor.com/project/ThibaultMontaufray/manager-litterature-fln7c)
[![Coverage Status](https://coveralls.io/repos/github/ThibaultMontaufray/Manager-Litterature/badge.svg?branch=master)](https://coveralls.io/github/ThibaultMontaufray/Manager-Litterature?branch=master)

# Usage

```csharp
Sentense sentense = new Sentense("Je suis une mouette", "ConversationId0123456789");
Console.WriteLine(sentense.Text);
Console.WriteLine(sentense.Subject);
Console.WriteLine(sentense.GroupCOD);
Console.WriteLine(sentense.GroupCOI);
Console.WriteLine(sentense.Adjectives);
Console.WriteLine(sentense.NomCommuns);
```
