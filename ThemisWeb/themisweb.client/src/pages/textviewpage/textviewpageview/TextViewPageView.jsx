import React, { useEffect, useState } from 'react'
import "./TextViewPageView.css"
import { useParams } from 'react-router-dom'
import TextRawContentContainer from './components/textrawcontentcontainer/TextRawContentContainer';
import TextFooter from './components/textfooter/TextFooter';
import { getTextRawText } from '../../../shared/network/text';
import { Spinner } from '@nextui-org/spinner';


const TextViewPageView = () => {
  const {id} = useParams();
  const [text, setText] = useState("");
  useEffect(() => {
    async function getText(){
      let textLink;
      await getTextRawText(id).then(s => s.json()).then(s => textLink = s.rawdatalink);
      fetch(textLink).then(s => s.text()).then(s => {setText(s)});
    }
    getText();
  }, [])
  return (
    <div className='TextViewPageView'>
      {text ? 
      <>
          <TextRawContentContainer text={text}></TextRawContentContainer>
          <TextFooter textId={id}></TextFooter>
      </>
      :
      <div className='flex items-center justify-center w-full h-[500px] '>
              <Spinner></Spinner>
      </div>
      }
    </div>
  )
}

export default TextViewPageView