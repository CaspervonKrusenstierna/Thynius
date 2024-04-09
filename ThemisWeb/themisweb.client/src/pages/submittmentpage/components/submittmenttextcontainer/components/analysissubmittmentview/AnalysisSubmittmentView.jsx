import React, { useCallback, useEffect, useRef, useState } from 'react'
import "./AnalysisSubmittmentView.css"
import InputIndexWidget from './components/inputindexwidget/InputIndexWidget';
import { GetThemisInputIndexRawText, ThemisInputsAdvance, sleep } from './utilities/Utilities';
import useInputs from './hooks/useInputs';
import getIndicatorTextCollection from './utilities/getIndicatorTextCollection';



const AnalysisSubmittmentView = (props) => {
  const [indicatorTextCollection, setIndicatorTextCollection] = useState();
  const [rawText, setRawText] = useState("");
  const [isPlaying, setIsPlaying] = useState(false);
  const [currIndex, setCurrIndex] = useState(0);
  const inputs = useInputs();

  useEffect(() => {
    async function runPlay(){
      let rawTextDummy = "";
      let currIndexDummy = currIndex;
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
      setCurrIndex(currIndexDummy-1);
    }
    if(isPlaying){
      if(inputs?.length == 0){
        return;
      }
      runPlay();
    }
  }, [isPlaying])

  
  const incIndex = useCallback(() => {
    setRawText(GetThemisInputIndexRawText(rawText, inputs, currIndex, 1))
    setCurrIndex(currIndex + 1);
  }, [rawText, currIndex, inputs])
  
  const setIndexBrutally = useCallback((index) => {
    console.log(inputs);
    console.log(index);
    setRawText(GetThemisInputIndexRawText("", inputs, 0, index));
    setCurrIndex(index);
  }, [inputs]);


  return (
    <div className='flex flex-col h-full relative'>
      <div className='AnalysisSubmittmentView'>
        {isPlaying ? rawText : indicatorTextCollection}
      </div>
      <InputIndexWidget setIsPlaying={setIsPlaying} isPlaying={isPlaying} decIndex={() => {setIndexBrutally(currIndex-1)}} incIndex={incIndex}></InputIndexWidget>
    </div>
  )
}

export default AnalysisSubmittmentView