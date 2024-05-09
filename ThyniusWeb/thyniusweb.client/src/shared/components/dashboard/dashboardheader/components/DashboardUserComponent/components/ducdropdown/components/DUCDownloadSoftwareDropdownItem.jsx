import React, { useState } from 'react'
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { getInstallerUrl } from '../../../../../../../../network/account'
import { useTranslation } from 'react-i18next'

const DUCDownloadSoftwareDropdownItem = () => {
  const [t, i18n] = useTranslation();
  async function onDownloadClick(){
    let installerUrl = await getInstallerUrl().then(s => s.text());
    window.open(installerUrl);
  }
  return (
        <Dialog>
            <DialogTrigger asChild>
                <button className='DUCDropdownItem'>{t("Download Software")}</button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>{t("Download Software")}</DialogTitle>
                </DialogHeader>
                <div className='h-20'></div>
                <div className='flex flex-row w-full justify-center'><Button size="lg" onClick={onDownloadClick}>{t("Download")}</Button></div>
            </DialogContent>
    </Dialog>
  )
}

export default DUCDownloadSoftwareDropdownItem