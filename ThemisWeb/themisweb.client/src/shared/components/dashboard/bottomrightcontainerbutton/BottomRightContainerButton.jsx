import React from 'react'
import "./BottomRightContainerButton.css"
import { Button } from "@/components/ui/button"
import { ArrowRight } from '../../../assets'


const BottomRightContainerButton = (props) => {
  return (
    <Button className="BottomRightContainerButton" size="default" onClick={props.onClick}>
        <p className='text-lg'>Bekräfta</p>
        <img className="h-6"src={ArrowRight}></img>
    </Button>
  )
}
export default BottomRightContainerButton