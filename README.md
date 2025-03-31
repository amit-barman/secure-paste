### 1. **App Overview**
***Secure Paste*** is a clipboard security app that monitors the clipboard content and replaces sensitive information with fake, random data to protect users' privacy. It aims to prevent accidental sharing of sensitive data by replacing it immediately when it is copied.

### 2. **Key Features**
Here's a list of core features of the app:

#### **Core Features**
- **Clipboard Monitoring**: Continuously monitor the clipboard for sensitive information.
- **Sensitive Data Detection**: Automatically detect sensitive information like:
  - Phone numbers
  - Credit card numbers
  - Social Security numbers
  - Email addresses
  - Access tokens/API keys
  - Bank account details
- **Random Replacement**: Replace detected sensitive data with fake, randomly generated values. For example:
  - Random phone numbers (valid format but not real)
  - Fake credit card numbers (that follow valid number patterns but are not real)
  - Randomly generated strings for access tokens
- **User Control**: Provide a way to enable and disable monitoring and the replacement of sensitive data.
- **Logging & Alerts**: Keep logs when sensitive data has been replaced and show notifications upon application startup and shutdown, allowing users to be aware of potential issues.

### 3. **Technology Stack**
    - .Net Windows Form App
    - C#