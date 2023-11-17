# MiskCore_Unity Documentation

MiskCore 旨在創造更方便操作的Unity的功能，集合可通用的功能，不與任何專案耦合，但耦合一些常用的套件。

## 耦合套件
* **DOTween**
* **UniRx**

## API

### Amplifier
用於增幅數值，類似於delegate，可自由移除增幅function，可同時掛載同樣的function。

同樣支援 AmplifierValue\<T, A1\>，以傳入一個參數的方式使用增幅器。

  * **class AmplifierValue\<T\>**

    創建一個增幅器。
    
    + **event void OnAmplifierChange ()**
      
      當增幅function更動時的事件。
    
    + **int Count (Func<T, T> func)**
      
      回傳func當前掛載數量。
    
    + **void Add (Func<T, T> func)**
      
      將增幅function加入增幅器中，若已加入，Invoke會呼叫多次。
    
    + **void Remove (Func<T, T> func)**
      
      移除增幅器內的function，若有兩個以上的同個function，則會減少一次呼叫次數。
    
    + **T Invoke (T value)**
      
      傳入一個數值，回傳增幅後的數值。
    
    + **ClearAmplifier ()**
      
      傳入一個數值，回傳增幅後的數值。
      
      
  * **class DecorateAmplifierValue\<T\> : AmplifierValue\<T\>**

    創建一個可裝飾的增幅器，實作Context自定義約束或操作規則。
    
    + **DT GetContext\<DT\> () where DT: DecorateAmplifierValue\<T\>.Context, new()**
      
      獲得一個裝飾，若已經存在則不建立。
    
    + **class Context**
    
        + **AmplifierValue\<T\> Amplifier**
      
          目標Amplifier。
      
      
  * **class MarkerContext\<T\> : DecorateAmplifierValue\<T\>.Context**

    以命名標示的規則將function放入增幅器，不可放入重複名稱。
    
    + **void TryAdd (string name, Func<T, T> func)**
      
      以字串key、function value的方式填入。
    
    + **void TryRemove(string name)**
      
      用名稱移除function。
      
      
  * **class TimerContext\<T\> : DecorateAmplifierValue\<T\>.Context**

    以命名標示的規則將具有時效性的function放入增幅器，不可放入重複名稱。
    
    + **void Set (string name, Func<T, T> func, float time)**
      
      以字串key、function value的方式填入，並設置有效時間，時間到會自動移除function。
    
    + **void TryRemove(string name)**
      
      用名稱移除function，而且立即停止計時。
    
