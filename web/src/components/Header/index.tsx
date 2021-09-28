/* eslint-disable @next/next/no-img-element */
import { HeaderContainer, Container, TransactionButton } from "./styles"

interface HeaderProps {
  setModalOpen: (newState: boolean) => void;
}

export function Header({ setModalOpen }: HeaderProps) {
  return (
    <HeaderContainer>
      <Container>
        <img src="/logo.svg" alt="Logo" />
        <TransactionButton
          onClick={() => setModalOpen(true)}
        >Nova transação</TransactionButton>
      </Container>
    </HeaderContainer>
  )
}