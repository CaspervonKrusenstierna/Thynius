import React, { useCallback, useContext, useEffect, useRef, useState } from 'react'
import InputIndexWidget from './components/inputindexwidget/InputIndexWidget';
import { GetThemisInputIndexRawText, ThemisInputsAdvance, clearSleeps, sleep } from './utilities/Utilities';
import useInputs from './hooks/useInputs';
import { submittmentInfoContext } from '../../../../SubmittmentPage';

const AnalysisSubmittmentView = () => {
  const [indicatorTextCollection, setIndicatorTextCollection] = useState();
  const [rawText, setRawText] = useState("");
  const [isPlaying, setIsPlaying] = useState(false);
  const [currIndex, setCurrIndex] = useState(0);
  const sleepsStorage = useRef([]);
  const currIndexDummy = useRef(-1);
  const currSession = useRef(0);
  const submittmentInfo = useContext(submittmentInfoContext);
  const inputs = useInputs();

  useEffect(() => {
    async function runPlay(){
      let rawTextDummy = rawText;
      currIndexDummy.current = currIndex;
      let timeOuts = [];
      while(isPlaying){
        let currInput = inputs[currIndexDummy.current];
        if(currIndexDummy.current == inputs.length){
          break;
        }
        if(currInput[1] == 8){
          currSession.current++;
          currIndexDummy.current++;
          continue
        }
        
        rawTextDummy = ThemisInputsAdvance(rawTextDummy, currInput);
        setRawText(rawTextDummy);
        try{
          let nextInput = inputs[currIndexDummy.current+1];
          if(!(nextInput[1] == 8)){
            await sleep(parseInt(nextInput[4])-parseInt(currInput[4]), sleepsStorage);
          }else{
            await sleep(5000, sleepsStorage);
          }
        }catch{
        }
        currIndexDummy.current++;
      }
      setIsPlaying(false);
    }
    if(isPlaying){
      if(inputs?.length == 0){
        return;
      }
      runPlay();
    }
    else{
      currIndexDummy.current++;
      clearSleeps(sleepsStorage);
      if(currIndexDummy.current != -1){
        setCurrIndex(currIndexDummy.current);
      }
    }
  }, [isPlaying])

  useEffect(() => { // runs when the user changes the currindex from the sidebar
    if(submittmentInfo.currIndex.current != -1){
      setIsPlaying(false);
      setIndexBrutally(submittmentInfo.currIndex.current);
    }
  }, [submittmentInfo.indexIsTouched])

  const incIndex = useCallback(() => {
    let currInput = inputs[currIndex];
    if(currInput[1] == 8){
      currSession.current++;
      setCurrIndex(currIndex + 1);
      return;
    }
    let currRaw = ThemisInputsAdvance(rawText, currInput);
    setRawText(currRaw);
    setCurrIndex(currIndex + 1);
  }, [rawText, currIndex, inputs])
  
  const setIndexBrutally = useCallback((index) => {
    let currRawInfo = GetThemisInputIndexRawText(rawText, inputs, 0, index);
    currSession.current = currRawInfo.session;
    setRawText(currRawInfo.text);
    setCurrIndex(index);
  }, [inputs]);


  return (
    <div className='flex flex-col h-full relative'>
      <div className="w-[90%] ml-[5%] h-full">
        <div className='h-[50px] text-[26px] font-bold flex flex-row justify-center items-center'>Session {currSession.current}</div>
        <p className="text-wrap mt-3 whitespace-pre">{rawText}</p>
      </div>
      <InputIndexWidget setIsPlaying={setIsPlaying} isPlaying={isPlaying} decIndex={() => {if(!isPlaying && !(currIndex==0)){setIndexBrutally(currIndex-1)}}} incIndex={() =>{if(!isPlaying && !(currIndex == inputs.length)){incIndex()}}}></InputIndexWidget>
    </div>
  )
}

export default AnalysisSubmittmentView