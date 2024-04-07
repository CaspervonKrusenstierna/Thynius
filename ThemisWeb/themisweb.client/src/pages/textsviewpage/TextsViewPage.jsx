import React from 'react'
import TextsViewPageView from './textsviewpageview/TextsViewPageView'
import { DashboardHeader } from "../../shared/components/dashboard"

const TextsViewPage = () => {
  return (
    <>
        <DashboardHeader></DashboardHeader>
        <div className='flex flex-column justify-center w-[100vw]'>
          <TextsViewPageView></TextsViewPageView>
        </div>
    </>
  )
}

export default TextsViewPage