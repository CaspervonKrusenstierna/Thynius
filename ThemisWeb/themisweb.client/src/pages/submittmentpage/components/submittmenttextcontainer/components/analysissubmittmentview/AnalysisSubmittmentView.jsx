import React, { useCallback, useContext, useEffect, useRef, useState } from 'react'
import "./AnalysisSubmittmentView.css"
import InputIndexWidget from './components/inputindexwidget/InputIndexWidget';
import { GetThemisInputIndexRawText, ThemisInputsAdvance, clearSleeps, sleep } from './utilities/Utilities';
import useInputs from './hooks/useInputs';
import getIndicatorTextCollection from './utilities/getIndicatorTextCollection';
import { submittmentInfoContext } from '../../../../SubmittmentPage';

let currIndexDummy = -1;
const AnalysisSubmittmentView = () => {
  const [indicatorTextCollection, setIndicatorTextCollection] = useState();
  const [rawText, setRawText] = useState("");
  const [isPlaying, setIsPlaying] = useState(false);
  const [currIndex, setCurrIndex] = useState(0);
  const submittmentInfo = useContext(submittmentInfoContext);
  const inputs = useInputs();

  useEffect(() => {
    async function runPlay(){
      let rawTextDummy = rawText;
      currIndexDummy = currIndex;
      let timeOuts = [];
      while(isPlaying){
        if(currIndexDummy == inputs.length){
          break;
        }
        rawTextDummy = ThemisInputsAdvance(rawTextDummy, inputs[currIndexDummy]);
        setRawText(rawTextDummy);
        currIndexDummy++;
        try{
          await sleep(parseInt(inputs[currIndexDummy+1][4])-parseInt(inputs[currIndexDummy][4]));
        }catch{
        }
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
      clearSleeps();
      if(currIndexDummy != -1){
        setCurrIndex(currIndexDummy);
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
    setRawText(GetThemisInputIndexRawText(rawText, inputs, currIndex, 1))
    setCurrIndex(currIndex + 1);
  }, [rawText, currIndex, inputs])
  
  const setIndexBrutally = useCallback((index) => {
    setRawText(GetThemisInputIndexRawText("", inputs, 0, index));
    setCurrIndex(index);
  }, [inputs]);


  return (
    <div className='flex flex-col h-full relative'>
      <div className='AnalysisSubmittmentView'>
        {rawText}
      </div>
      <InputIndexWidget setIsPlaying={setIsPlaying} isPlaying={isPlaying} decIndex={() => {if(!isPlaying && !(currIndex==0)){setIndexBrutally(currIndex-1)}}} incIndex={() =>{if(!isPlaying && !(currIndex == inputs.length-1)){incIndex()}}}></InputIndexWidget>
    </div>
  )
}

export default AnalysisSubmittmentView