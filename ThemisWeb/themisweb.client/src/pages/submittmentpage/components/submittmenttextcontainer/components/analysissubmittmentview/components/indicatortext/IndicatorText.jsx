import React from 'react'
import "./IndicatorText.css"

const IndicatorText = (props) => {

    function textType(type, content) {
        switch(type) {
            case 0:
            return <p className='IndicatorText-Normal'>{content}</p>;
            case 2:
            return <p className='IndicatorText-Paste'>{content}</p>;
        }
    }
  return (
    <>
    {
        textType(props.type, props.text)
    }
    </>
  )
}

export default IndicatorText