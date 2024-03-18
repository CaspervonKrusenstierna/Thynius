
import { Header } from "../../shared/components/index"
import MainContentContainer from "../../shared/components/maincontentcontainer/MainContentContainer"
import WaveBackgroundBox from "../../shared/components/wavebackgroundbox/WaveBackgroundBox"
const HomePage = () => {
  return (
      <>
          <WaveBackgroundBox></WaveBackgroundBox>
          <Header></Header>  
          <MainContentContainer></MainContentContainer>

      </>
  )
}

export default HomePage