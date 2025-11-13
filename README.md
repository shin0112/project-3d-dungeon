# 3D dugeon

---

## 📑 목차

- [실행 방법](#실행-방법)
- [발표 자료 & 문서](#발표-자료--문서)
- [프로젝트 개요](#프로젝트-개요)
- [프로젝트 설명](#프로젝트-설명)
  - [1. Tech Stack](#1-tech-stack)
  - [2. Architecture](#2-architecture)
- [기능 설명](#기능-설명)
  - [1. 움직임 시스템](#1-움직임-시스템)
  - [2. 시점 변경](#2-시점-변경)
  - [3. 인벤토리](#3-인벤토리)
  - [4. 아이템](#4-아이템)
  - [5. 환경 상호작용](#5-환경-상호작용)
- [트러블슈팅](#트러블슈팅)

---

## [실행 방법]

> todo

## [발표 자료 & 문서]

> todo

## [참고 자료]

> todo

## [프로젝트 개요]

| 항목       | 내용                             |
| ---------- | -------------------------------- |
| 프로젝트명 | 3d Dungeon                       |
| 주제       | 3d 던전 만들기                   |
| 개발 인원  | 총 1명 (개발자)                  |
| 개발 기간  | 2025.11.09 ~ 2025.11.13 (총 5일) |
| 개발 목적  | Unity 3d 실습                    |

## [프로젝트 설명]

### 1. Tech Stack

| 구분            | 기술                                                                                                                  |
| --------------- | --------------------------------------------------------------------------------------------------------------------- |
| Language        | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white">                  |
| Framework       | <img src="https://img.shields.io/badge/unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black">                  |
| IDE             | <img src="https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white"> |
| Version Control | <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white">                |
| Design          | <img src="https://img.shields.io/badge/Figma-F24E1E?style=for-the-badge&logo=figma&logoColor=white">                  |
| Documentation   | <img src="https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white">                |

---

### 2. Architecture

```plaintext
// todo
```

---

## [기능 설명]

### 1. 움직임 시스템

- 이동 (WASD)
- 점프, 이단 점프 (Space)
- 대시 (C) - 스테미나 소모

<video autoplay muted loop width="640" height="360" src="Docs/Video/Movement.mp4"></video>

### 2. 시점 변경

- F5로 1인칭 ↔ 3인칭 전환
- 카메라 회전(마우스 Look)

<video autoplay muted loop width="640" height="360" src="Docs/Video/ChangePerspective.mp4"></video>

### 3. 인벤토리

- 인벤토리 (Tap or 버튼 클릭)
- 슬롯 UI 표시
- 아이템 상태 반영

<video autoplay muted loop width="640" height="360" src="Docs/Video/ToggleInventory.mp4"></video>

### 4. 아이템

- 장비 아이템 착용 (E)
- 버프 아이템 사용 (Q)
- 능력치 변화 적용

<video autoplay muted loop width="640" height="360" src="Docs/Video/EquipItem.mp4"></video>
<video autoplay muted loop width="640" height="360" src="Docs/Video/BuffItem.mp4"></video>

### 5. 환경 상호작용

- 점프대
  - 충돌 시 `AddForce` 적용
- 벽 타기
  - `Raycast`로 벽 감지

<video autoplay muted loop width="640" height="360" src="Docs/Video/JumpAndClimb.mp4"></video>

## [트러블슈팅]

> 개발 중 발생한 주요 이슈 및 해결 과정을 정리했습니다.  
> 각 항목은 별도 TIL 또는 블로그 포스트로 링크됩니다.

| 주제               | 해결 요약                                                    | 링크                                                                                                                                                                                                                             |
| ------------------ | ------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 인벤토리 UI 만들기 | `RectTransform` 사용 및 새로운 코루틴 생성하기               | [🔗 UI Image 화살표 뒤집기](https://velog.io/@shin0112/Unity-UI-Image%EC%97%90%EC%84%9C-%ED%99%94%EC%82%B4%ED%91%9C-%EB%92%A4%EC%A7%91%EA%B8%B0-Flip-%EC%97%86%EC%9D%B4-%EB%B0%A9%ED%96%A5-%EC%A0%84%ED%99%98%ED%95%98%EA%B8%B0) |
| 벽 등반 구현 과정  | `RayCast`감지 + velocity 덮어쓰기 문제 해결 + WallSlide 추가 | [🔗 벽 등반(Climb) 구현 과정](https://velog.io/@shin0112/Unity-%EB%B2%BD-%EB%93%B1%EB%B0%98Climb-%EA%B5%AC%ED%98%84-%EA%B3%BC%EC%A0%95)                                                                                          |

---
