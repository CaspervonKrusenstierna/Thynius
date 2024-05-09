import React from 'react'
import "./BottomRightContainerButton.css"
import { Button } from "@/components/ui/button"
import { useTranslation } from 'react-i18next'


const BottomRightContainerButton = (props) => {
  const [t, i18n] = useTranslation();
  return (
    <Button className="BottomRightContainerButton" size="default" onClick={props.onClick}>
        <p className='text-lg'>{t("Confirm")}</p>
    </Button>
  )
}
export default BottomRightContainerButton