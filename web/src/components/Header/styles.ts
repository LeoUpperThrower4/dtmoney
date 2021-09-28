import styled from 'styled-components'

export const HeaderContainer = styled.header`
  width: 100%;
  margin: 0 auto;

  background-color: var(--blue);
  height: 13rem;
`
export const Container = styled.div`

  max-width: 1120px;
  margin: 0 auto;

  padding-top: 2rem;

  display: flex;
  align-items: center;
  justify-content: space-between;
`
export const TransactionButton = styled.button`
  cursor: pointer;

  padding: .75rem 2rem;

  color: white;
  font-weight: 600;

  border: 0;
  border-radius: .3rem;
  background-color: #6933FF;
  
  transition: filter .2s;
  &:hover {
    filter: brightness(.9);
  }
`