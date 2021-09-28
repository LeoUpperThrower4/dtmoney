import type { AppProps } from 'next/app'
import { Footer } from '../components/Footer'
import { FaunaInfoProvider } from '../hooks/useFaunaInfo'

import '../styles/globals.scss'

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <FaunaInfoProvider>
      <Component {...pageProps} />
      <Footer />
    </FaunaInfoProvider>)
}
export default MyApp
